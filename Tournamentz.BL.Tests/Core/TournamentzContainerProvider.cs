namespace Tournamentz.BL.Tests.Core
{
    using Autofac;

    public static class TournamentzContainerProvider
    {
        private static IContainer _container;
        private static readonly object Lock = new object();

        public static IContainer Instance
        {
            get
            {
                if (_container == null)
                {
                    lock (Lock)
                    {
                        if (_container == null)
                        {
                            _container = new TournamentzContainerBuilder().Build();
                        }
                    }
                }

                return _container;
            }
        }
    }
}
