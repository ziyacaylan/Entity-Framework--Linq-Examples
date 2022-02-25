using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

//using MySQL.Data.EntityFrameworkCore.Extensions;

namespace ConsoleApp_FW_6_0_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            //AddProduct();
            AddProducts();
            //GetAllProducts();
            //GetAllProductById(13);
            //GetAllProductByName("Samsung");
            //UpdateProduct();
            //DeleteProduct(5);
            //GetAllProducts();
        }

        static void DeleteProduct(int id)
        {
            using (var db = new ShopContext())
            {
                var p = new Product() { Id = 12 };
                // db.Products.Remove(p);
                db.Entry(p).State = EntityState.Deleted; // ilgili objenin silindiği change tracking e haber verrir bu şekildede silme işlemi gerçekleştirilir.
                db.SaveChanges();
                Console.WriteLine("Ürün Silindi...!");



                //var p= db.Products.FirstOrDefault(i=>i.Id==id);
                //if(p!=null)
                //{
                //    db.Products.Remove(p);
                //    db.SaveChanges();
                //    Console.WriteLine("Ürün Silindi...!");
                //}
            }
        }


        static void UpdateProduct()
        {

            using (var db = new ShopContext())
            {
                var p = db.Products.Where(i => i.Id == 1).FirstOrDefault();

                if (p != null)
                {
                    // bu şekilde update de bitin kolon güncellenmektedir. Diğer alttaki metodlar daha faydalı olacaktır.
                    p.Price = 3909;
                    db.Products.Update(p);
                    db.SaveChanges();
                    Console.WriteLine("Güncelleme Yapıldı...!");
                }
            }




            //using (var db=new ShopContext())
            //{
            //    var entity = new Product() { Id = 1 };
            //    db.Products.Attach(entity);
            //    entity.Price = 3300;
            //    db.SaveChanges();
            //    Console.WriteLine("Güncelleme Yapıldı...!");
            //}



            //using (var db=new ShopContext())
            //{
            //    // change trackig
            //    var p = db.Products
            //              .Where(i => i.Id == 5)
            //              .FirstOrDefault();
            //    if (p != null)
            //    {
            //        p.Price *= 1.5m;
            //        db.SaveChanges();

            //        Console.WriteLine("Güncelleme Yapıldı...!");
            //    }
            //}
        }

        static void GetAllProductByName(string name)
        {
            using (var context = new ShopContext())
            {
                var products = context
                            .Products
                            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                            .Select(p => new { p.Name, p.Price })
                            .ToList();

                foreach (var p in products)
                {
                    Console.WriteLine($"name: {p.Name} price: {p.Price}");
                }
                // .Where(p => p.Price > 2000 && p.Name == "Samsung S7")
            }
        }
        static void GetAllProductById(int Id)
        {
            using (var context = new ShopContext())
            {
                var result = context
                            .Products
                            .Where(p => p.Id == Id)
                            .Select(p => new { p.Name, p.Price })
                            .FirstOrDefault();

                Console.WriteLine($"name: {result.Name} price: {result.Price}");

            }
        }

        static void GetAllProducts()
        {
            using (var context = new ShopContext())
            {
                var products = context
                    .Products
                    .Select(p => new { p.Name, p.Price })
                    .ToList();

                foreach (var p in products)
                {
                    Console.WriteLine($"name: {p.Name} price: {p.Price}");
                }
            }
        }

        static void AddProducts()
        {
            using (var db = new ShopContext())
            {
                var products = new List<Product>()
                {
                    new Product { Name = "Samsung S5", Price = 2000 },
                    new Product { Name = "Samsung S6", Price = 3000 },
                    new Product { Name = "Samsung S7", Price = 4000 },
                    new Product { Name = "Samsung S8", Price = 5000 },
                };


                db.Products.AddRange(products);
                db.SaveChanges();
                Console.WriteLine("Kayıtlar eklendi...!");




            }
        }

        static void AddProduct()
        {
            using (var db = new ShopContext())
            {
                var product = new Product { Name = "Samsung S11", Price = 11000 };

                db.Products.Add(product);
                db.SaveChanges();
                Console.WriteLine("Kayıtlar eklendi...!");




            }
        }
    }

    public class ShopContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=ShopDB_1;Integrated Security=SSPI;");

            //optionsBuilder.UseMySql(@"Server=localhost;port=3306;database=ShopDb;user=root;password=admin;SSL Mode = None;");
            //optionsBuilder.UseMySQL(@"server=localhost;port=3306;database=shopdb;user=root;password=admin;");

            //optionsBuilder.UseMySQL(@"Server=localhost;Database=shopdb;Uid=root;Pwd=admin;");
        }
    }

    public class Product
    {
        // primary key
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }


        public decimal Price { get; set; }

        public int CategoryId { get; set; }

    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
