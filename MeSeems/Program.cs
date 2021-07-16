using MeSeems.Helpers;
using MeSeems.Models;
using MeSeems.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MeSeems
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService user = new UserService();
            InvitationService invitation = new InvitationService();
            
            Util.Write("Iniciando MeSeems", '#');
            
            Util.Write("[OBTENDO DADOS DOS USUÁRIOS]", '-');
            var users = user.GetInfoUsers(@"D:\Vinic\Python\MeSeems\MeSeems\Data\app_users.csv");
            
            Util.Write("[OBTENDO DADOS DOS CONVITES]", '-');
            invitation.GetInfoInvitations(@"D:\Vinic\Python\MeSeems\MeSeems\Data\app_invitations.csv", users);

            Util.Write("Concluido", ' ');
            Util.Write("", '#');
            Console.ReadKey();
        }
    }
}
