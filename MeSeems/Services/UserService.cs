using MeSeems.Helpers;
using MeSeems.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MeSeems.Services
{
    public class UserService
    {
        public List<Users> GetInfoUsers(string filePath)
        {
            List<Users> users = new List<Users>();
            using (var reader = new StreamReader(filePath))
            {

                int count = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (count > 0 && line.Length > 0)
                    {
                        var values = line.Split(',');
                        users.Add(new Users(values[0], values[1], values[2], values[3], values[4], values[5]));
                    }
                    count++;
                }
            }

            #region [Quantos usuários se cadastraram em todo o período do dataset fornecido ?]
            Util.Write($"Quantos usuários se cadastraram em todo o período do dataset fornecido ?", ' ');
            Util.Write($"R: {users.Count}", null);
            #endregion [Quantos usuários se cadastraram em todo o período do dataset fornecido ?]

            #region [Quantos usuários não finalizaram o cadastro?]
            Util.Write($"Quantos usuários não finalizaram o cadastro?", ' ');
            Util.Write($"R: {users.Where(x => string.IsNullOrEmpty(x.Region) || string.IsNullOrEmpty(x.SocialClass) || (x.Birth == null || x.Birth == DateTime.MinValue)).Count()}", null);
            #endregion [Quantos usuários não finalizaram o cadastro?]

            #region [Qual a tendência da curva de volume de cadastros?]
            Util.Write($"Qual a tendência da curva de volume de cadastros?", ' ');
            foreach (var item in users.Select(x => x.CreationDate.Month).Distinct())
                Util.Write($"{CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item)} = {users.Where(x => x.CreationDate.Month == item).Count()}", null);
            #endregion [Qual a tendência da curva de volume de cadastros?]

            #region [Quais os perfis demográficos que mais se cadastram no app? E os que menos se cadastram?]
            Util.Write($"Quais os perfis demográficos que mais se cadastram no app? E os que menos se cadastram?", ' ');
            var agrupamento = users.GroupBy(x => x.Region)
             .Select(x => new
             {
                 Regiao = string.IsNullOrEmpty(x.Key) ? "Não Informado" : x.Key,
                 Qtd = x.Count()
             });

            foreach (var item in agrupamento.OrderBy(x => x.Qtd))
                Util.Write($"{item.Regiao} = {item.Qtd}", null);
            #endregion [Quais os perfis demográficos que mais se cadastram no app? E os que menos se cadastram?]


            #region [Qual a taxa de usuários com perfil demográfico completo?]
            Util.Write($"Qual a taxa de usuários com perfil demográfico completo?", ' ');
            var groupFullProfile = users.GroupBy(x => x.Region)
                .Select(x => new
                {
                    fullProfile = string.IsNullOrEmpty(x.Key) ? false : true,
                    Qtd = x.Count()
                });


            var rate = Math.Round(Convert.ToDecimal(((double)groupFullProfile.Where(X => X.fullProfile == true).Sum(x => x.Qtd) / users.Count()) * 100), 2);
            Util.Write($"R: {rate}%", null);
            #endregion [Qual a taxa de usuários com perfil demográfico completo?]

            return users;
        }
    }
}