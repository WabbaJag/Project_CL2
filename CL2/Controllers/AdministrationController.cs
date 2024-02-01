using CL2.Extensions;
using CL2.Models;
using CL2.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Controllers
{

    [Authorize]
    public class AdministrationController : BaseController
    {      
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;


        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        //CREAR ROLES DE USUARIO, SOLO EL ROL ADMIN TIENE PERMISO
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole(){
          return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    BasicNotification("", NotificationType.Success, "Rol ha sido creado exitosamente");
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    BasicNotification("", NotificationType.Error, "Rol no ha sido creado");

                }
            }
            return View(model);
        }



        //VER TODOS LOS ROLES DE USUARIO, SOLO EL ROL ADMIN TIENE PERMISO
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }


        //EDITAR ROLES, SOLO EL ROL ADMIN TIENE PERMISO
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                BasicNotification("", NotificationType.Error, "Rol no encontrado");
                return RedirectToAction("ListRoles");
            }

            var model = new EditRoleViewModel
            { 
            Id = role.Id,
            RoleName = role.Name};

            foreach (ApplicationUser user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
                
            }
            return View(model);
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                BasicNotification("", NotificationType.Error, "Rol no encontrado");
                return RedirectToAction("ListRoles");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    BasicNotification("", NotificationType.Success, "Rol Actualizado Correctamente");
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                   
                    BasicNotification("", NotificationType.Error, "El Rol no ha sido actualizado");
                }
                return View(model);
            }           
        }


        //AGREGAR O QUITAR USUARIOS DEL ROL, SOLO EL ROL ADMIN TIENE PERMISO
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;       
            var role = await roleManager.FindByIdAsync(roleId);
            ViewBag.roleName = role.Name;

            if (role == null)
            {
                BasicNotification("", NotificationType.Error, "Rol no encontrado");
                return RedirectToAction("ListRoles");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                BasicNotification("", NotificationType.Error, "Rol no encontrado");
                return RedirectToAction("ListRoles");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    
                        continue;
                        else
                        BasicNotification("", NotificationType.Success, "Rol Actualizado Correctamente");
                        return RedirectToAction("EditRole", new { Id = roleId });                  
                }
            }
            BasicNotification("", NotificationType.Success, "Rol Actualizado Correctamente");
            return RedirectToAction("EditRole", new { Id = roleId });
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


        //Lista de todos los Usuarios
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        //Editar Usuario
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                BasicNotification("", NotificationType.Error, "Usuario no encontrado");
                return RedirectToAction("ListUsers");
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = userRoles
            };

            return View(model);

        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                BasicNotification("", NotificationType.Error, "Usuario no encontrado");
                return RedirectToAction("ListUsers");
            }
            else
            {
                user.Email = model.Email;

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    BasicNotification("", NotificationType.Success, "Usuario Actualizado Correctamente");
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            

        }

        //Manejo de los Roles de un Usuario
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                BasicNotification("", NotificationType.Error, "Usuario no encontrado");
                return RedirectToAction("ListUsers");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name

                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                BasicNotification("", NotificationType.Error, "Usuario no encontrado");
                return RedirectToAction("ListUsers");
            }


            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                BasicNotification("", NotificationType.Error, "No fue posible actualizar el Usuario");
                return RedirectToAction("ListUsers");
            }

            result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                BasicNotification("", NotificationType.Error, "No fue posible actualizar el Usuario");
                return RedirectToAction("ListUsers");
            }
            BasicNotification("", NotificationType.Success, "Usuario Actualizado Correctamente");
            return RedirectToAction("EditUser", new { Id = userId});
        }


        //ELIMINAR USUARIOS

        // GET: Fraccions/Delete/5
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                BasicNotification("", NotificationType.Error, "Usuario no encontrado");
                return RedirectToAction("ListUsers");
            }

            var user = await userManager.FindByIdAsync(id);
          
            if (user == null)
            {
                BasicNotification("", NotificationType.Error, "Usuario no encontrado");
                return RedirectToAction("ListUsers");
            }

            return View(user);
        }

        // POST: Fraccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            try
            {
               
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    BasicNotification("", NotificationType.Success, "Usuario Eliminado Correctamente");
                    return RedirectToAction("ListUsers");
                }

                             
                
            }
            catch
            {
                BasicNotification("", NotificationType.Error, "No es posible eliminar el Usuario");

                return RedirectToAction("ListUsers");
            }
            return RedirectToAction("ListUsers");
        }


    }
}
