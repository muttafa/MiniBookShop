using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using omerd.Server.Models;
using omerd.Server.ViewModels;
using System.Linq;

namespace omerd.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cart : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public Cart(ApplicationDBContext context)
        {
            _dbContext = context;
        }



        [HttpPost]
        [Route("addCart")]
        public ActionResult AddCart([FromBody] CartViewModel cart)
        {
            try
            {
                if (cart != null)
                {
                    CartModel newCart = new CartModel(); 

                    newCart.ProductID = cart.ProductID;
                    newCart.UserId = cart.UserID;
                    newCart.Count = 1;
                    newCart.AddDate = DateTime.Now;
                    newCart.isActive = 1;
                    if (newCart != null)
                    {
                        _dbContext.CartModel.Add(newCart);
                        _dbContext.SaveChanges();

                        return Ok(new { success = true });
                    }
                    else
                    {
                        return BadRequest(new {success= false, message = "Sepete eklenirken bir hata meydana geldi" });
                    }
                   
                }
                else
                {
                    return BadRequest(new { success = false, message = "Sepete eklenirken bir hata meydana geldi" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getCartDetail/{userID}")]
        public JsonResult GetCartDetail(int userID)
        {
            try
            {
                if (userID != 0)
                {
                    var cartDetail = (from cart in _dbContext.CartModel
                                      join product in _dbContext.Products on cart.ProductID equals product.ProductID into productJoinTable
                                      from products in productJoinTable.DefaultIfEmpty()

                                      join user in _dbContext.Users on cart.UserId equals user.UserID into userJoinTable
                                      from users in userJoinTable.DefaultIfEmpty()

                                      join productPhotos in _dbContext.ProductPhotos on products.ProductID equals productPhotos.ProductID into productPhotosJoinTable
                                      from productsPhoto in productPhotosJoinTable.DefaultIfEmpty()


                                      where cart.isActive == 1 && cart.UserId == userID

                                      group cart by new
                                      {
                                          productID = products.ProductID,
                                          productName = products.ProductName,
                                          productPrice = products.Price,
                                          ProductPhotoUrl = productsPhoto != null ? productsPhoto.ProductPhotoUrl : null,
                                      } into groupedCart
                                      select new
                                      {
                                          
                                          productID = groupedCart.Key.productID,
                                          productName = groupedCart.Key.productName,
                                          productPrice = groupedCart.Key.productPrice,
                                          totalPrice = groupedCart.Sum(cartItem => cartItem.Count * groupedCart.Key.productPrice),
                                          orderCount = groupedCart.Sum(cartItem => cartItem.Count),
                                          ProductPhotoUrl = groupedCart.Key.ProductPhotoUrl,
                                      }).ToList();





                    if (cartDetail != null)
                    {
                        return Json(new { data = cartDetail, success = true });
                    }
                    else
                    {
                        return Json(new { message = "Sepet boş", success = false , isUser = true });
                    }

                }
                else
                {
                    return Json(new { message = "Geçersiz kullanıcı", success = false ,isUser = false });
                }

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false });
            }

        }

        [HttpPost]
        [Route("removeItem")]
        public ActionResult RemoveItem([FromBody] RemoveItem item)
        {
            try
            {
                var currentCart = _dbContext.CartModel.Where(x => x.UserId == item.userID && x.ProductID == item.productID).ToList();

                if (currentCart != null)
                {
                    foreach (var removeItem in currentCart)
                    {
                        _dbContext.CartModel.Remove(removeItem);
                    }
                    _dbContext.SaveChanges();
                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest(new { success = false , message = "Ürün buluınamadı" });
                }



            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
                throw;
            }
            return Ok();
        }


        [HttpPost]
        [Route("submitPayment")]
        public ActionResult SubmitPayment([FromBody] CreditCartViewModel paymentDetails)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (paymentDetails != null)
                    {
                        var currentUserCart = _dbContext.CartModel
                            .Where(x => x.UserId == paymentDetails.UserID && x.isActive == 1)
                            .ToList();

                        foreach (var item in currentUserCart)
                        {
                            item.isActive = 0;

                            var product = _dbContext.Products.Find(item.ProductID);
                            if (product != null)
                            {
                                product.StockQuantity -= item.Count;
                            }
                        }

                        CreditCard cc = new CreditCard
                        {
                            UserID = paymentDetails.UserID,
                            CVV = paymentDetails.CVV,
                            Expiration = paymentDetails.Expiration,
                            KartName = paymentDetails.KartName,
                            CardNo = paymentDetails.CardNo,
                        };

                        var card = _dbContext.CreditCard.FirstOrDefault(x => x.CardNo == cc.CardNo);

                        if (cc != null && card == null)
                        {
                            _dbContext.CreditCard.Add(cc);
                        }

                        _dbContext.SaveChanges();
                    }

                    transaction.Commit();
                    return Ok(new {success=true});
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(new { success = false, message = "Ödeme işlemi gerçekleştirilemedi. Hata: " + ex.Message });
                }
            }
        }


        [HttpGet]
        [Route("deleteCart/{userID}")]
        public ActionResult DeleteCart(int userID)
        {
            try
            {
                if (userID != 0)
                {
                    var cart = _dbContext.CartModel.Where(x => x.UserId == userID).ToList();
                    foreach (var item in cart)
                    {
                        _dbContext.Remove(item);
                    }
                    _dbContext.SaveChanges();
                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest(new { success = false , message = "kullanıcı bulunamadı"});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });

                throw;
            }

            }
        }

    }
