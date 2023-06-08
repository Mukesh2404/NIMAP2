using NIMAP2.Interface;
using NIMAP2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NIMAP2.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private IProduct Product;
        public ProductController(IProduct pro)
        {
            Product = pro;
        }



        ProductContext db = new ProductContext();
        // GET: Product

        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync(int PageNumber = 1)
        {
            var parameter = new[]
                {
                 new SqlParameter ("@PageNbr",PageNumber),      //[0]index
                 new SqlParameter ("@TotalPages",SqlDbType.Int) { Direction = ParameterDirection.Output}    //[1]index
                };
            var data = await db.Products.SqlQuery("Execute spGetpageRow @PageNbr,@Totalpages output", parameter).ToListAsync();

            ViewBag.Totalpages = (int)parameter[1].Value;
            return View(data);

        }


        public ActionResult Create()
        {
            var identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims;
            var identifier = claims.Where(model => model.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            string name = identifier.Value;

            var data = db.SignupTbl.FirstOrDefault(model => model.UserName == name);

            Product product = new Product()
            {
                UserId = data.id
            };
            return View(product);

        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync(Product p)
        {
            var data = await Product.Create(p);
            if (data)
            {
                TempData["Message"] = "<script>alert('Item Created successfully')</script>";
                return RedirectToAction("Index");
            }
            return View();
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(int id)
        {
            var data = await Product.GetProductByID(id);

            return View(data);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(Product p)
        {
            var data = await Product.Edit(p);
            if (data)
            {
                TempData["Message"] = "<script>alert('Data Edited Successfully')</script>";
                return RedirectToAction("Index");
            }
            return View();
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var detailsvalue = await Product.GetProductByID(id);
            return View(detailsvalue);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var data = await Product.GetProductByID(id);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(Product pro)
        {
            var data = await Product.Delete(pro.Id);

            if (data)
            {
                TempData["Message"] = "<script>alert('Product Deleted Successfully')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "<script>alert('Product Not Deleted')</script>";
                ModelState.Clear();
            }
            return View();
        }
    }
}