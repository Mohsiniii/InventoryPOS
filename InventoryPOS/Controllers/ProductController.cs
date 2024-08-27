using InventoryPOS.Controllers;
using InventoryPOS.Data;
using InventoryPOS.Models;
using Microsoft.AspNetCore.Mvc;

public class ProductController : BaseController
{
    private ProductDAL _productDAL;
    private CategoryDAL _categoryDAL;
    private BrandDAL _brandDAL;

    public ProductController(IConfiguration configuration)
    {
        _productDAL = new ProductDAL(configuration.GetConnectionString("DefaultConnection"));
        _categoryDAL = new CategoryDAL(configuration.GetConnectionString("DefaultConnection"));
        _brandDAL = new BrandDAL(configuration.GetConnectionString("DefaultConnection"));
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult All()
    {
        var products = _productDAL.GetAll();
        return View(products);
    }

    public IActionResult Add()
    {
        var categories = _categoryDAL.GettAll();
        var brands = _brandDAL.GettAll();
        var product = new Product
        {
            categories = categories,
            brands = brands
        };
        return View(product);
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        if (_productDAL.create(product))
        {
            TempData["SuccessMessage"] = "Product added successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to add the product. Please try again.";
        }
        return RedirectToAction("All");
    }

    [HttpGet]
    public IActionResult AddVariant(int id)
    {
        var product = _productDAL.GetByID(id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    public IActionResult AddVariant(string size_weight, decimal unit_price, int productID)
    {
        var addVariant = _productDAL.addVariant(productID, size_weight, unit_price);

        if (addVariant)
        {
            TempData["SuccessMessage"] = "Variant added successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to add the variant.";
        }

        return RedirectToAction("All", "Product");
    }

    [HttpGet]
    public IActionResult Remove(int id)
    {
        bool success = _productDAL.Remove(id);

        if (success)
        {
            TempData["SuccessMessage"] = "Product removed successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to remove the product.";
        }

        return RedirectToAction("All", "Product");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _productDAL.GetByID(id);
        product.categories = _categoryDAL.GettAll();
        product.brands = _brandDAL.GettAll();

        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        var result = _productDAL.update(product);

        if (result)
        {
            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToAction("All", "Product");
        }
        else
        {
            TempData["ErrorMessage"] = "Unable to update the product. Please try again.";
        }

        product.categories = _categoryDAL.GettAll();
        product.brands = _brandDAL.GettAll();
        return View(product);
    }
}
