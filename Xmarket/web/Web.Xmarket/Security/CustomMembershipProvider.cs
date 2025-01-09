﻿

namespace Web.Xmarket.Security
{
    using System;
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Security;

    using System.Linq;
    using Web.Xmarket.DataAccess.Seguridad;

    public class CustomMembershipProvider : MembershipProvider
    {
        #region Properties

        private int _cacheTimeoutInMinutes = 30;

        #endregion


        /// <summary>
        /// Initialize values from web.config.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // Set Properties
            int val;
            if (!string.IsNullOrEmpty(config["cacheTimeoutInMinutes"]) && Int32.TryParse(config["cacheTimeoutInMinutes"], out val))
                _cacheTimeoutInMinutes = val;

            // Call base method
            base.Initialize(name, config);
        }

        /// <summary>
        /// valida el usuario que se encuentra en la base de datos.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override  bool ValidateUser(string username, string password)
        {
            HttpContext.Current.Session.Remove("mensaje_validateuser");
            AccountData accountData = new AccountData();
            var result = accountData.ValidateUser(username, password);
            if (result == "OK")
                return true;

            HttpContext.Current.Session["mensaje_validateuser"] = result;
            return false;
        }

        /// <summary>
        /// Valida el usuario coorporativo.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidateUserAD(string username, string password)
        {
            HttpContext.Current.Session["mensaje_validateuser"] = "OK";
            return true;
        }


        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
             var cacheKey = string.Format("UserData_{0}", username);

          //  if (HttpRuntime.Cache[cacheKey] != null)
            //   return (CustomMembershipUser)HttpRuntime.Cache[cacheKey];

           
            AccountData accountData = new AccountData();
            var resultado = accountData.ObtenerUsuario(username);

            HttpContext.Current.Session["mensaje_validateuser"] = "";

            if (resultado == null) return null;
            if (resultado.ListaRoles.Any() == false) return null;

            //elinando mensaje de errores
            HttpContext.Current.Session["mensaje_validateuser"] = null;

            //var usuario =
            //    new Usuario(
            //        resultado.Hit.IdUsuario,
            //        resultado.Hit.Nombre,
            //        resultado.Hit.ApellidoPaterno + " "+resultado.Hit.ApellidoMaterno,
            //        resultado.Hit.CodigoTxt,
            //        resultado.Hit.Correo,
            //        1,
            //        resultado.Hit.Estado=="0"?0:1,
            //        1,
            //        resultado.Hit.FechaRegistro,
            //        resultado.Hit.FechaRegistro,
            //        resultado.Hit.FechaRegistro,
            //        0,
            //        0,
            //        0,
            //        "",
            //       "",
            //        ""
            //        );

            //foreach (var rol in resultado.ListaRoles)
            //{
            //    usuario.Perfiles.Add(new Perfil() { IdPerfil = rol.Rol_int_id, NombrePerfil = rol.Rol_str_alias, DashBoard = rol.rol_str_dashboard });
            //}

            //SessionWrapper user = new SessionWrapper();
            //user.Usuario = usuario;

           // GestionSession o_SessionGestion = new GestionSession();
           // user.Session = o_SessionGestion.RegistrarSesion(Convert.ToInt32(usuario.Idusuario));

            var membershipUser = new CustomMembershipUser(null);



            //  HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);

            return membershipUser;
        }


        public override string ApplicationName
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new System.NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }



        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new System.NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new System.NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new System.NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new System.NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}