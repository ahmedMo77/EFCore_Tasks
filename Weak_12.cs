
// 1
public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(ConString);
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
}

var context = new AppDbContext();
var blog = context.Blogs.Find(1);
var posts = blog.Posts;


// ====================================================================

// 2
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int ProductId { get; set; }

    public virtual Product Product { get; set; }
}
var context = new AppDbContext()
var productsWithOrders = context.Products
    .Include(p => p.Orders)
    .ToList();

//==============================================

// 3.
public class ProductProxy : Product
{
    private bool _ordersLoaded;
    private ICollection<Order> _orders;

    public override ICollection<Order> Orders
    {
        get
        {
            if (!_ordersLoaded)
            {
                _orders = context.Entry(this)
                    .Collection(p => p.Orders)
                    .Query()
                    .ToList();
                _ordersLoaded = true;
            }
            return _orders;
        }
    }
}

//====================================================

// 4.
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}

var customers = context.Customers
        .Include(c => c.Orders)
        .ThenInclude(o => o.Product)
        .AsSplitQuery()
        .ToList();






