using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FarmDBMongoV1.App_Start;
using FarmDBMongoV1.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Globalization;

namespace FarmDBMongoV1
{
    public class AccountsDropdown
    {
        public int AccountNumber { get; set; }
        public string AccountDesc { get; set; }
    }

    public class GetAccounts
    {
        MongoContext context = new MongoContext();
        public SelectList AccountsSelectList(int inputSelectedValue)
        {
            var collection = context.db.GetCollection<Account>("Accounts");
        var query =
            from e in collection.AsQueryable<Account>()
            .Where(x => x.Active==true)
            select new { e.AccountNumber, e.AccountName };
        var AccountsList = query.ToList();
        List<AccountsDropdown> viewList = new List<AccountsDropdown>();
        foreach (var item in AccountsList)
        {
            AccountsDropdown a = new AccountsDropdown();
            a.AccountNumber = item.AccountNumber;
            a.AccountDesc = String.Concat(item.AccountNumber, " - ", item.AccountName);
            viewList.Add(a);
        }
            SelectList s = new SelectList(viewList, "AccountNumber", "AccountDesc",inputSelectedValue);
            return(s);
        }

        public SelectList AccountTypesSelectList(string inputSelectedValue)
        {
            List<SelectListItem> Listsi = new List<SelectListItem>();
            Listsi.Add(new SelectListItem() { Text = "Expenses", Value = "Expenses" });
            Listsi.Add(new SelectListItem() { Text = "Revenue", Value = "Revenue" });
            SelectList s = new SelectList(Listsi, "Value", "Text", inputSelectedValue);
            return (s);
        }

        public SelectList AccountPersonSelectList(string inputSelectedValue)
        {
            List<SelectListItem> Listsi = new List<SelectListItem>();
            Listsi.Add(new SelectListItem() { Text = "Allen", Value = "Allen" });
            Listsi.Add(new SelectListItem() { Text = "Mark", Value = "Mark" });
            Listsi.Add(new SelectListItem() { Text = "Mike", Value = "Mike" });
            SelectList s = new SelectList(Listsi, "Value", "Text", inputSelectedValue);
            return (s);
        }

        public DateTime? StrToDate(string val)
        {
            DateTime dateValue;
            if (DateTime.TryParse(val, out dateValue))
            {
                return (dateValue);
            }
            else
            {
                return null;
            }
        }

        public List<TransactionsWithAccounts> TA (string firstDay, string lastDay, string personid)
        {
            DateTime fromDate = Convert.ToDateTime(firstDay);
            DateTime toDate = Convert.ToDateTime(lastDay);
            var trans = context.db.GetCollection<Transaction>("Transactions")
            .Aggregate()
            .Lookup("Accounts", "AccountNumber1", "AccountNumber", "Account_docs").ToList();
            List<TransactionsWithAccounts> myColl = new List<TransactionsWithAccounts>();
                foreach (BsonDocument bd in trans)
                {
                    TransactionsWithAccounts ta = BsonSerializer.Deserialize<TransactionsWithAccounts>(bd);
                     myColl.Add(ta);
                }
             var result =
            myColl.AsQueryable()
            .Where(x => x.TransactionDate >= fromDate)
            .Where(x => x.TransactionDate <= toDate)
            .Where(x => x.Account_docs.Select(user => user.AccountPerson.AccountPerson1).Contains(personid))
            .Where(x => x.Account_docs.Select(user => user.Active).Contains(true))
            .OrderBy(x => x.TransactionDate);
           return result.ToList();
        }

        public List<Account> A()
        {
            List<Account> accounts = context.db.GetCollection<Account>("Accounts")
            .AsQueryable().ToList();
            return accounts;
        }
    }
}