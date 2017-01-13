using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logcorner.Models;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

namespace Logcorner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public ActionResult PostToPayBox(string item, string amount)
        {
            ViewBag.actionURl = "https://preprod-tpeweb.paybox.com/cgi/MYchoix_pagepaiement.cgi";
            PayBoxModel paybox = new Models.PayBoxModel();
            paybox.PBX_PORTEUR  ="leyegora@yahoo.fr";
            paybox.PBX_TOTAL = amount;
            paybox.PBX_CMD = String.Format("LogCorner_{0}_{1}", item, amount);
            paybox.PBX_SITE = ConfigurationManager.AppSettings["Paybox.Site"];
            paybox.PBX_RANG = ConfigurationManager.AppSettings["Paybox.Rang"];
            paybox.PBX_IDENTIFIANT = ConfigurationManager.AppSettings["Paybox.Identifiant"];
            paybox.PBX_DEVISE = ConfigurationManager.AppSettings["Paybox.Devise"];
            paybox.PBX_RETOUR = ConfigurationManager.AppSettings["Paybox.Retour"];
         
            paybox.PBX_HASH = "SHA512";
            paybox.PBX_TIME = DateTime.Now.ToString();

            paybox.PBX_TYPECARTE = ConfigurationManager.AppSettings["Paybox.TypeCarte"];
            paybox.PBX_TYPEPAIEMENT = ConfigurationManager.AppSettings["Paybox.TypePaiement"];
         
            string clearMessage = String.Format("PBX_SITE={0}&PBX_RANG={1}&PBX_IDENTIFIANT={2}&PBX_TOTAL={3}&PBX_DEVISE={4}&PBX_CMD={5}&PBX_PORTEUR={6}&PBX_RETOUR={7}&PBX_HASH={8}&PBX_TIME={9}&PBX_TYPEPAIEMENT={10}&PBX_TYPECARTE={11}", paybox.PBX_SITE, paybox.PBX_RANG, paybox.PBX_IDENTIFIANT, paybox.PBX_TOTAL, paybox.PBX_DEVISE, paybox.PBX_CMD, paybox.PBX_PORTEUR, paybox.PBX_RETOUR, paybox.PBX_HASH, paybox.PBX_TIME, paybox.PBX_TYPEPAIEMENT, paybox.PBX_TYPECARTE);
            string secretKeyString  = ConfigurationManager.AppSettings["Paybox.SecretKeyString"];
         //   string hmac = GenerateHMAC(clearMessage, secretKeyString);
            paybox.PBX_HMAC = GenerateHMAC(clearMessage, secretKeyString);
            return View(paybox);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clearMessage"></param>
        /// <param name="secretKeyString"></param>
        /// <returns></returns>
        private string GenerateHMAC(string clearMessage, string secretKeyString)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] messageBytes = encoder.GetBytes(clearMessage);
         
            byte[] secretKeyBytes = new byte[secretKeyString.Length / 2];
            for (int index = 0; index < secretKeyBytes.Length; index++)
            {
                string byteValue = secretKeyString.Substring(index * 2, 2);
                secretKeyBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
           
            HMACSHA512 hmacsha512 = new HMACSHA512(secretKeyBytes);

           
            byte[] hashValue = hmacsha512.ComputeHash(messageBytes);

         
            string hmac = "";
            foreach (byte x in hashValue)
            {
                hmac += String.Format("{0:x2}", x);
            }

      
            return  hmac.ToUpper();
        }
    }
}
