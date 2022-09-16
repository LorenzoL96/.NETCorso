using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCorso.Models.Enums;

namespace NETCorso.Models.ValueTypes
{
    public class Money
    {
        private decimal amount = 0;

        public Money(Currency currency, decimal amount){
            Amount = amount;
            Currency = currency;
        }
        public Money(): this(Currency.EUR, 0.00m){}

        public decimal Amount{
            get {
                return amount;
            }
            set {
                if(value < 0){
                    throw new InvalidOperationException("The amount canno be negative");
                }
                amount = value;
            }
        }

        public Currency Currency{get; set;}

        public override bool Equals(object obj){
            var money = obj as Money;
            return  money != null &&
                    Amount == money.Amount &&
                    Currency == money.Currency;
        }

        public override int GetHashCode(){
            return HashCode.Combine(Amount, Currency);
        }

        public override string ToString(){
            return $"{Currency} {Amount: #.00}";
        }
                
    }
}