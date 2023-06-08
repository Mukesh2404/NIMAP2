using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using NIMAP2.viewmodel;

namespace NIMAP2.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryList> CategoriesList { get; set; }
        public DbSet<SignUp> SignupTbl { get; set; }


        public string cs = ConfigurationManager.ConnectionStrings["productcontext"].ConnectionString;


        //Category


        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetAllCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        Category cat = new Category();
                        cat.Id = dr.GetFieldValue<int>(0);
                        cat.CategoryName = dr.GetFieldValue<string>(1);
                        cat.IsActive = dr.GetFieldValue<bool>(2);

                        categories.Add(cat);
                    }
                }
                return categories;
            }
        }

        public async Task<bool> CreateCategory(Category cat)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spCreatecategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
                cmd.Parameters.AddWithValue("@Active", cat.IsActive);
                con.Open();
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public async Task<Category> GetCategoryByID(int id)
        {
            Category cat = new Category();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetCategoryById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (await dr.ReadAsync())
                    {
                        cat.Id = dr.GetFieldValue<int>(0);
                        cat.CategoryName = dr.GetFieldValue<string>(1);
                        cat.IsActive = dr.GetFieldValue<bool>(2);
                    }
                }

            }
            return cat;
        }




        public async Task<bool> EditCategory(Category cat)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spEditCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", cat.Id);
                cmd.Parameters.AddWithValue("@Name", cat.CategoryName);
                cmd.Parameters.AddWithValue("@Active", cat.IsActive);
                con.Open();
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeletecategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public async Task<bool> DeActiveCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeActiveCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public async Task<bool> ActiveCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spActiveCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> AddProductInCategory(int id, int CatId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spAddProductToCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pid", id);
                cmd.Parameters.AddWithValue("@Cid", CatId);
                conn.Open();
                int i = await cmd.ExecuteNonQueryAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public async Task<List<Product>> GetProductList(int id)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spShowCatProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CatagoryId", id);
                con.Open();
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        Product Pro = new Product();
                        Pro.Id = dr.GetFieldValue<int>(0);
                        Pro.ProductName = dr.GetFieldValue<string>(1);
                        Pro.ProductDescription = dr.GetFieldValue<string>(2);
                        Pro.ProductPrice = dr.GetFieldValue<int>(3);

                        products.Add(Pro);
                    }
                }
                return products;

            }


        }



        public async Task<List<Report>> GetReport(int id)
        {
            List<Report> ree = new List<Report>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", id);
                con.Open();
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        Report re = new Report();
                        re.UserName = dr.GetFieldValue<string>(0);
                        re.CatName = dr.GetFieldValue<string>(1);
                        re.ProName = dr.GetFieldValue<string>(2);
                        re.price = dr.GetFieldValue<int>(3);

                        ree.Add(re);
                    }

                }
                return ree;
            }
        }

        public List<Report> GetAllreport()
        {
            List<Report> re = new List<Report>();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spReports", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Report ree = new Report();
                        ree.UserName = dr.GetFieldValue<string>(0);
                        ree.CatName = dr.GetFieldValue<string>(1);
                        ree.ProName = dr.GetFieldValue<string>(2);
                        ree.price = dr.GetFieldValue<int>(3);
                        re.Add(ree);
                    }
                }
                return re;
            }
        }

        //public System.Data.Entity.DbSet<InventryShop.viewmodel.Report> Reports { get; set; }
    }
}
}