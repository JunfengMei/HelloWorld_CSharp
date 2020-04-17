using System;

delegate int ChangeNumber(int n);
namespace DelegateApp
{
	public class TestDelegate
	{
		static int num = 10;

		public static int AddNum(int p)
		{
			num += p;
			return num;
		}

		public static int MulNum(int q)
		{
			num *= q;
			return num;
		}

		public static int getNum()
		{
			return num;
		}

		static void Main2(String[] args)
		{
			//创建委托实例
			ChangeNumber cn1 = new ChangeNumber(AddNum);
			ChangeNumber cn2 = new ChangeNumber(MulNum);
			//使用委托对象调用方法
			cn1(25);
			Console.WriteLine("Num={0}", getNum());
			cn2(5);
			Console.WriteLine("Num={0}", getNum());
			Console.ReadKey();
		}

	}
}