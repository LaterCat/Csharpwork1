using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*程序设计题：请使用委托实现信用卡用户定时还款功能
 * 
本题的应用场景解释：
    用户有一张信用卡，信用卡有一个总额度；
    每个月会有信用卡账单显示月消费总额，月消费总额是小于信用卡总额度的；
    用户有若干储蓄卡，可选择某张储蓄卡进行还款；
    还款是指从储蓄卡中划走信用卡的月消费总额到信用卡；
    如果储蓄卡余额不足则还款动作不成功。
要求如下：      ①必须使用委托
                ②事件的触发方式是每个月的到期还款日；*/

namespace Csharpwork1 {

    public class Delegate    //委托事件类
    {
        public delegate void payBackMoney(AccountCard accountcart, CreditCard creditcard);
        public event payBackMoney payBackMoneyEvent;

        public void isPayTrue(AccountCard accountcart, CreditCard creditcard)
        {
            if(payBackMoneyEvent != null)
                payBackMoneyEvent(accountcart, creditcard);
            
        }
    }

    /*public class Bank            //银行类
    {

    }*/

    public class CreditCard           //信用卡类
    {
        private int totalAmount = 10000;  //总额度
        private int totalMonthlyAmount;   //月消费总额
        private string repayDate;            //还款日期

        public CreditCard()
        {
            string year = DateTime.Now.ToString("yyyy-MM-dd").Split('-')[0];
            string month = DateTime.Now.ToString("yyyy-MM-dd").Split('-')[1];
            string day = DateTime.Now.ToString("yyyy-MM-dd").Split('-')[2];
            Console.WriteLine("还款日期：" + month + "月" + 15 +"日");
        }
        //public string a =(DateTime.Now.ToString("yyyy-MM-dd"));
        //public int a = int.Parse((DateTime.Now.ToString("yyyy-MM-dd")).Split('-')[0]);

        public int gettotalMonthlyAmount()
        {
            return this.totalMonthlyAmount;
        }

        public void settotalMonthlyAmount()
        {
            Console.Write("本月消费：");
            int totalMonthlyAmount = Convert.ToInt32(Console.ReadLine());
            if (totalMonthlyAmount >= 0 && totalMonthlyAmount < totalAmount)
                this.totalMonthlyAmount = totalMonthlyAmount;
            else
                Console.WriteLine("已超过信用卡总额度！");
        }

        public void setrepayDate()
        {
            Console.Write("当前日期：");
            string repayDate = Console.ReadLine();
            this.repayDate = repayDate;
        }
    }

    public class AccountCard           //储蓄卡类
    {
        private int Balance;          //储蓄卡余额

        public int getBalance()
        {
            return this.Balance;
        }

        public void setBalance()
        {
            Console.Write("储蓄卡余额：");
            int Balance = Convert.ToInt32(Console.ReadLine());
            this.Balance = Balance;
        }

        public void reCharge(AccountCard accountcart)
        {
            int money = Convert.ToInt32(Console.ReadLine());
            this.Balance += money;
            Console.WriteLine("充值成功！当前余额："+ accountcart.getBalance());
        }

        public void charge(AccountCard accountcart, CreditCard creditcard)
        {
            if (accountcart.getBalance() > creditcard.gettotalMonthlyAmount())
            {
                this.Balance = accountcart.getBalance() - creditcard.gettotalMonthlyAmount();
                Console.WriteLine("还款成功,余额 " + (this.Balance) +" 元");
            }
            else
            {
                Console.Write("余额不足，还款失败,请充值 ：");
                reCharge(accountcart);
                charge(accountcart, creditcard);
            }
        }
    }


    public class Test         //测试类
    {
        static void Main(string[] args)
        {
            Delegate dlg = new Delegate();
            CreditCard creditcard = new CreditCard();
            AccountCard accountcart = new AccountCard();






            int a = 0;
            creditcard.setrepayDate();
            accountcart.setBalance();
            creditcard.settotalMonthlyAmount();

            dlg.payBackMoneyEvent += new Delegate.payBackMoney(accountcart.charge);
            dlg.isPayTrue(accountcart, creditcard);
            Console.ReadKey();
        }
        
        
    }

}
