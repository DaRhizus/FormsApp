using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index(string searchProduct, string category)
    {
        var products = Repository.Products;
        var categories = Repository.Categories;

        if(!string.IsNullOrEmpty(searchProduct)){
            ViewBag.SearchProduct = searchProduct;
            products = products.Where(p => p.Name.ToLower().Contains(searchProduct)).ToList();
        }

        if(!string.IsNullOrEmpty(category)){
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        var model = new ProductViewModule{
            Products = products,
            Categories = categories,
            SelectedCategory = category
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult SubmitProduct(){
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitProduct(Product model, IFormFile ImageFile){
        var allowedExtensions = new[]{".jpg", ".jpeg", ".png",};
        
        var extension = Path.GetExtension(ImageFile.FileName);
        var imageFileName = $"{CharacterNormalizer.NormalizeTurkishChars(model.Name.ToLower())}{extension}";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", imageFileName);

        if(!allowedExtensions.Contains(extension)){
            ModelState.AddModelError("", "Lütfen sadece jpg/jpeg veya png türünde bir resim yükleyiniz");
        }

        if(ModelState.IsValid){
            using(var stream = new FileStream(path, FileMode.Create)){
                await ImageFile.CopyToAsync(stream);
            }
            model.Image = imageFileName;  
            Repository.CreateProduct(model);
            return RedirectToAction("Index");
        }
        else{
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
            return View(model);
        }
    }

    public IActionResult EditProduct(int? id){
        if(id == null){
            return NotFound(id);
        }

        var entity = Repository.Products.FirstOrDefault(x => x.Id == id);

        if(entity == null){
            return NotFound(id);
        }

        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(int id, Product model, IFormFile? ImageFile){
        if(id != model.Id){
            return NotFound(id);
        }
    
        if(ModelState.IsValid){
            if(ImageFile != null){
                var extension = Path.GetExtension(ImageFile.FileName);
                var imageFileName = $"{CharacterNormalizer.NormalizeTurkishChars(model.Name.ToLower())}{extension}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", imageFileName);
                var allowedExtensions = new[]{".jpg", ".jpeg", ".png",};
                if(!allowedExtensions.Contains(extension)){
                    ModelState.AddModelError("", "Lütfen sadece jpg/jpeg veya png türünde bir resim yükleyiniz");
                }
                using(var stream = new FileStream(path, FileMode.Create)){
                    await ImageFile.CopyToAsync(stream);
                }
                model.Image = imageFileName;
            }
        }
            Repository.Edit(model);
            return View("Index");
    }

    
    public IActionResult Delete(int? id){
        if(id==null){
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(x => x.Id == id);
        var itemCategory = Repository.Categories.FirstOrDefault(x => x.CategoryId == entity.CategoryId );
        ViewBag.Category = itemCategory.CategoryName;
        return View("DeleteItem", entity);
    }


    [HttpPost]
    public IActionResult DeleteItem(int? id, int Id){
        if(id!=Id){
            return NotFound();
        }

        var entity = Repository.Products.FirstOrDefault(x => x.Id == Id);

        if(entity==null){
            return NotFound();
        }

        Repository.Delete(entity);
        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult EditProducts(ProductViewModule model){
        
        foreach(var product in model.Products){
            Repository.EditIsActive(product);
        }
        
        return RedirectToAction("Index");
    }
}
