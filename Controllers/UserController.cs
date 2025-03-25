using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public IActionResult Index(string searchName = null)
    {
        var users = string.IsNullOrEmpty(searchName)
            ? userlist
            : userlist.Where(u => u.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();

        return View(users);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Devuelve un error 404 si el usuario no existe.
        }
        return View(user); // Devuelve el usuario a la vista Details.
    }

    // GET: User/Create
    public ActionResult Create()
    {
        return View(); // Devuelve la vista para crear un nuevo usuario.
    }

    // POST: User/Create
    [HttpPost]
    public IActionResult Create(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        user.Id = UserController.userlist.Count + 1; // Simulación de ID único
        userlist.Add(user);
        return RedirectToAction("Index");
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Devuelve un error 404 si el usuario no existe.
        }
        return View(user); // Devuelve el usuario a la vista Edit.
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound(); // Devuelve un error 404 si el usuario no existe.
        }

        if (ModelState.IsValid)
        {
            existingUser.Name = user.Name; // Actualiza los datos del usuario.
            existingUser.Email = user.Email;
            return RedirectToAction(nameof(Index)); // Redirige a la acción Index.
        }
        return View(user); // Si hay errores de validación, devuelve la vista con los datos ingresados.
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Devuelve un error 404 si el usuario no existe.
        }
        return View(user); // Devuelve el usuario a la vista Delete.
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Devuelve un error 404 si el usuario no existe.
        }

        userlist.Remove(user); // Elimina el usuario de la lista.
        return RedirectToAction(nameof(Index)); // Redirige a la acción Index.
    }
}
