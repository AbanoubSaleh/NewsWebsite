//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v13.8.0+a486d5d
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	// Mixin Content Type with alias "accountBasePage"
	/// <summary>Account Base Page</summary>
	public partial interface IAccountBasePage : IPublishedElement
	{
		/// <summary>Meta Description</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string MetaDescription { get; }

		/// <summary>Meta Title</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string MetaTitle { get; }
	}

	/// <summary>Account Base Page</summary>
	[PublishedModel("accountBasePage")]
	public partial class AccountBasePage : PublishedElementModel, IAccountBasePage
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		public new const string ModelTypeAlias = "accountBasePage";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<AccountBasePage, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public AccountBasePage(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Meta Description
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("metaDescription")]
		public virtual string MetaDescription => GetMetaDescription(this, _publishedValueFallback);

		/// <summary>Static getter for Meta Description</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetMetaDescription(IAccountBasePage that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "metaDescription");

		///<summary>
		/// Meta Title: title of the page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("metaTitle")]
		public virtual string MetaTitle => GetMetaTitle(this, _publishedValueFallback);

		/// <summary>Static getter for Meta Title</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.8.0+a486d5d")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetMetaTitle(IAccountBasePage that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "metaTitle");
	}
}
