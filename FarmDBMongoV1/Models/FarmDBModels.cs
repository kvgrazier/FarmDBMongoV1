using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FarmDBMongoV1.Models
{
    [Serializable]
    public class Account
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public int AccountNumber { get; set; }
        [BsonElement]
        public string AccountName { get; set; }
        [BsonElement]
        public string TaxFormRef { get; set; }
        [BsonElement]
        public bool Active { get; set; }
        [BsonElement]
        public AccountPerson AccountPerson { get; set; }
        [BsonElement]
        public AccountSubType AccountSubType { get; set; }
    }

    [Serializable]
    public class AccountPerson
    {
        [BsonElement]
        public string AccountPerson1 { get; set; }
    }

    [Serializable]
    public class AccountSubType
    {
        [BsonElement]
        public string AccountSubType1 { get; set; }
        [BsonElement]
        public int SortOrder { get; set; }
        [BsonElement]
        public virtual AccountType AccountType { get; set; }
    }

    [Serializable]
    public class AccountType
    {
        [BsonElement]
        public string AccountType1 { get; set; }
    }

    [Serializable]
    public class Transaction
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public int TransactionID { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [BsonElement]
        [BsonRepresentation(BsonType.DateTime)]
        public System.DateTime TransactionDate { get; set; }
        [BsonElement]
        public string TransactionDescription { get; set; }
        [BsonElement]
        public int AccountNumber1 { get; set; }
        [BsonElement]
        public decimal Account1Amount { get; set; }
    }

    [Serializable]
    public class TransactionsWithAccounts
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public int TransactionID { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [BsonElement]
        [BsonRepresentation(BsonType.DateTime)]
        public System.DateTime TransactionDate { get; set; }
        [BsonElement]
        public string TransactionDescription { get; set; }
        [BsonElement]
        public int AccountNumber1 { get; set; }
        [BsonElement]
        public decimal Account1Amount { get; set; }
        [BsonElement]
        public List<Account> Account_docs { get; set; }
    }

    public class FilteredTransactions
    {
        public Nullable<int> AccountNumber { get; set; }
        public string AccountName { get; set; }
        public decimal Total { get; set; }
        public string AccountSubType  { get; set; }
        public int SortOrder { get; set; }
        public string AccountType   { get; set; }
        public string AccountPerson  { get; set; }
        public int Count { get; set; }
    }

}