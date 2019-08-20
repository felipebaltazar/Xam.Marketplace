using System;
using System.Linq.Expressions;

namespace Xam.Marketplace.Mappings
{
    public abstract class Mapping<TSource, TDestination>
    {
        #region Fields

        private readonly Func<TSource, TDestination> _projectFunc;

        #endregion

        #region Constructor

        protected Mapping()
        {
            Projection = BuildProjection();
            _projectFunc = Projection.Compile();
        }

        #endregion

        #region Public Methods

        public Expression<Func<TSource, TDestination>> Projection { get; }

        public TDestination Project(TSource source) =>
            _projectFunc(source);

        #endregion

        #region Protected

        protected abstract Expression<Func<TSource, TDestination>> BuildProjection();

        #endregion
    }
}
