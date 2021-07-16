using MeSeems.Helpers;
using MeSeems.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MeSeems.Services
{
    public class InvitationService
    {
        private const decimal ValorConvite = 1;
        public void GetInfoInvitations(string filePath, List<Users> users)
        {
            List<Invitations> invitations = new List<Invitations>();
            using (var reader = new StreamReader(filePath))
            {
                int count = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (count > 0 && line.Length > 0)
                    {
                        var values = line.Split(',');
                        invitations.Add(new Invitations(values[0], values[1], values[2]));
                    }
                    count++;
                }
            }

            #region [Quais são os usuários que mais convidam no app?]
            Util.Write($"Quais são os usuários que mais convidam no app?", ' ');
            var group = invitations.GroupBy(x => x.SenderAppUserId)
                .Select(x => new
                {
                    AppUserId = x.Key,
                    Qtd = x.Count()
                });

            foreach (var item in group.OrderByDescending(x => x.Qtd).Take(10))
                Util.Write($"{item.AppUserId} = {item.Qtd}", null);
            #endregion [Quais são os usuários que mais convidam no app?]


            #region [Quantos usuários em média cada usuário convida?]
            Util.Write($"Quantos usuários em média cada usuário convida?", ' ');
            Util.Write($"R: {invitations.Count() / invitations.Select(x => x.SenderAppUserId).Distinct().Count()}", null);
            #endregion [Quantos usuários em média cada usuário convida?]


            #region [Qual o custo mensal (em reais) de aquisição via convite?]
            Util.Write($"Qual o custo mensal (em reais) de aquisição via convite?", ' ');
            var invitationsTemp = from i in invitations
                                  join u in users on i.RecipientAppUserId equals u.AppUserId
                                  select new
                                  {
                                      i.RecipientAppUserId,
                                      u,
                                      i.SenderAppUserId,
                                      i.Status
                                  };

            var groupMonthlyCost = invitationsTemp.Where(x => x.Status).GroupBy(x => x.u.CreationDate.Month)
                .Select(x => new
                {
                    Month = x.Key,
                    MonthlyValue = x.Count() * ValorConvite
                });

            foreach (var item in groupMonthlyCost.OrderBy(x => x.Month))
                Util.Write($"{CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Month)} = R${item.MonthlyValue}", null);
            #endregion [Qual o custo mensal (em reais) de aquisição via convite?]

        }
    }
}