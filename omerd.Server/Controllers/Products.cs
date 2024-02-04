using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace omerd.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDBContext _dbContext; 

        public ProductsController(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("getProducts")]
        public JsonResult getProducts()
        {
            var productList = _dbContext.Products.ToList();

            if (productList.Any())
            {
                var allProducts = (from pr in productList
                                   join ph in _dbContext.ProductPhotos on pr.ProductID equals ph.ProductID into newJoinTable
                                   from prph in newJoinTable.DefaultIfEmpty()

                                   select new
                                   {
                                       ProductID = prph != null ? prph.ProductID : (int?)null,
                                       ProductPhotoID = prph != null ? prph.ProductPhotoID : (int?)null,
                                       ProductPhotoUrl = prph != null ? prph.ProductPhotoUrl : null,
                                       Star = pr != null ? pr.Star : 0,
                                       Price = pr.Price,
                                       StockQuantity = pr != null ? pr.StockQuantity : (int?)null,
                                       ProductName = pr != null ? pr.ProductName : null
                                   }).ToList();
                return Json(new { success = true, data = allProducts });

            }
            else
            {
                return Json(new { success = false, data = "Elimizde ürün kalmadı" });
            }

        }

        [HttpGet]
        [Route("getBestProducts")]
        public JsonResult getBestProducts()
        {
            try
            {
                var top5Products = _dbContext.Products
                .OrderByDescending(p => p.Star)
                .Take(5);

                if (top5Products.Any())
                {
                    var topProducts = (from pr in top5Products
                                       join ph in _dbContext.ProductPhotos on pr.ProductID equals ph.ProductID into newJoinTable
                                       from prph in newJoinTable.DefaultIfEmpty()

                                       select new
                                       {
                                           ProductID = prph != null ? prph.ProductID : (int?)null,
                                           ProductPhotoID = prph != null ? prph.ProductPhotoID : (int?)null,
                                           ProductPhotoUrl = prph != null ? prph.ProductPhotoUrl : null,
                                           Star = pr != null ? pr.Star : 0,
                                           Price = pr.Price,
                                           StockQuantity = pr != null ? pr.StockQuantity : (int?)null,
                                           ProductName = pr != null ? pr.ProductName : null
                                       }).ToList();

                    return Json(new { success = true, data = topProducts });  
                }
                else
                {
                    return Json(new { success = false, message = "Güzel ürünümüz yok" });
                }



            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpGet]
        [Route("getLatestProducts")]
        public JsonResult GetLatestProducts()
        {
            try
            {
                var latestProducts = _dbContext.Products.OrderByDescending(x => x.CreateDate).ToList().Take(3);

                if (latestProducts.Any())
                {
                    var latest = (from pr in latestProducts
                                  join ph in _dbContext.ProductPhotos on pr.ProductID equals ph.ProductID into newJoinTable
                                       from prph in newJoinTable.DefaultIfEmpty()

                                       select new
                                       {
                                           ProductID = prph != null ? prph.ProductID : (int?)null,
                                           ProductPhotoID = prph != null ? prph.ProductPhotoID : (int?)null,
                                           ProductPhotoUrl = prph != null ? prph.ProductPhotoUrl : null,
                                           Star = pr != null ? pr.Star : 0,
                                           Price = pr.Price,
                                           StockQuantity = pr != null ? pr.StockQuantity : (int?)null,
                                           ProductName = pr != null ? pr.ProductName : null
                                       }).ToList();

                    return Json(new { success = true, data = latest });
                }
                else {
                    return Json(new { success = false, message = "Yeni ürün eklenmedi" });
                     }   
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            throw;
            }
        }

        [HttpPost]
        [Route("filterProducts")]
        public JsonResult FilterProducts([FromBody] ProductsViewModel filterValue)
        {
            try
            {
                if (filterValue != null)
                {
                    var filteredProducts = _dbContext.Products
                        .Where(x =>
                            (filterValue.ProductName == null || x.ProductName.Contains(filterValue.ProductName)) &&
                            (!filterValue.Price.HasValue || x.Price >= filterValue.Price) &&
                            (!filterValue.StockQuantity.HasValue || x.StockQuantity >= filterValue.StockQuantity) &&
                            (!filterValue.Star.HasValue || x.Star >= filterValue.Star) &&
                            (!filterValue.CreateDate.HasValue || x.CreateDate > filterValue.CreateDate)
                        )
                        .ToList();

                    if (filteredProducts.Any())
                    {
                        var filtered = (from pr in filteredProducts
                                        join ph in _dbContext.ProductPhotos on pr.ProductID equals ph.ProductID into newJoinTable
                                           from prph in newJoinTable.DefaultIfEmpty()

                                           select new
                                           {
                                               ProductID = prph != null ? prph.ProductID : (int?)null,
                                               ProductPhotoID = prph != null ? prph.ProductPhotoID : (int?)null,
                                               ProductPhotoUrl = prph != null ? prph.ProductPhotoUrl : null,
                                               Star = pr != null ? pr.Star : 0,
                                               Price = pr.Price,
                                               StockQuantity = pr != null ? pr.StockQuantity : (int?)null,
                                               ProductName = pr != null ? pr.ProductName : null
                                           }).ToList();
                        return Json(new { success = true, data = filtered });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Ürün bulunamadı" });
                    }

                }
                else
                {
                    var filteredProducts = _dbContext.Products.ToList();

                    if (filteredProducts.Any())
                    {
                        var filtered = (from pr in filteredProducts
                                        join ph in _dbContext.ProductPhotos on pr.ProductID equals ph.ProductID into newJoinTable
                                        from prph in newJoinTable.DefaultIfEmpty()

                                        select new
                                        {
                                            ProductID = prph != null ? prph.ProductID : (int?)null,
                                            ProductPhotoID = prph != null ? prph.ProductPhotoID : (int?)null,
                                            ProductPhotoUrl = prph != null ? prph.ProductPhotoUrl : null,
                                            Star = pr != null ? pr.Star : 0,
                                            Price = pr.Price,
                                            StockQuantity = pr != null ? pr.StockQuantity : (int?)null,
                                            ProductName = pr != null ? pr.ProductName : null
                                        }).ToList();
                        return Json(new { success = true, data = filtered });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Ürün bulunamadı" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }

    }
}