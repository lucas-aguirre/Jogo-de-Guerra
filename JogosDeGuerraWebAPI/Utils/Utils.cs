using JogosDeGuerraModel;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JogosDeGuerraWebAPI.Utils 
{
    public class Utils : Controller
{
        public static Usuario ObterUsuarioLogado(JogosDeGuerraModel.ModelJogosDeGuerra ctx)
        {
            var ident = System.Web.HttpContext.Current.User.Identity;
            if (ident.IsAuthenticated)
            {
                var usuario = ctx.Usuarios.Where(u => u.Email == ident.Name).SingleOrDefault();
                if (usuario == null)
                {
                    Usuario u = new Usuario();
                    u.Email = ident.Name;
                    ctx.Usuarios.Add(u);
                    ctx.SaveChanges();
                    return u;
                }
                return usuario;
            }
            return null;
        }

        public static bool ObterLogado()
        {
            var ident = System.Web.HttpContext.Current.User.Identity;
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return true;
            }
            return false;
        }

        public void DeslogarUsuario(IOwinContext ctx)
        {
            
            ctx.Authentication.SignOut();
        }

    }
}