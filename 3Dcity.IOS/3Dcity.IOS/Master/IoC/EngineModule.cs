using System;
using System.Collections.Generic;
using Ninject.Modules;

namespace WindowsGame.Master.IoC
{
	public class EngineModule : NinjectModule
	{
		private readonly IDictionary<Type, Type> data;

		public EngineModule(IDictionary<Type, Type> data)
		{
			this.data = data;
		}

		public override void Load()
		{
			foreach (KeyValuePair<Type, Type> info in data)
			{
				Type t1 = info.Key;
				Type t2 = info.Value;

				Bind(t1).To(t2).InSingletonScope();
			}
		}
	}
}