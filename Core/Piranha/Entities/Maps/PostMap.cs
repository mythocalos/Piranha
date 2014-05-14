﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Piranha.Entities.Maps
{
	/// <summary>
	/// Entity map for the post.
	/// </summary>
	public class PostMap : EntityTypeConfiguration<Post>
	{
		public PostMap() {
			ToTable("post") ;

			HasKey(p => new { p.Id, p.IsDraft }) ;
			Property(p => p.Id).HasColumnName("post_id") ;
			Property(p => p.IsDraft).HasColumnName("post_draft") ;
			Property(p => p.TemplateId).HasColumnName("post_template_id") ;
			Property(p => p.PermalinkId).HasColumnName("post_permalink_id") ;
			Property(p => p.AllowRss).HasColumnName("post_rss") ;
			Property(p => p.Title).HasColumnName("post_title").IsRequired().HasMaxLength(128) ;
			Property(p => p.Excerpt).HasColumnName("post_excerpt").HasMaxLength(512) ;
			Property(p => p.Body).HasColumnName("post_body") ;
			Property(p => p.Keywords).HasColumnName("post_keywords").HasMaxLength(128) ;
			Property(p => p.Description).HasColumnName("post_description").HasMaxLength(255) ;
			Property(p => p.AttachmentsJson).HasColumnName("post_attachments") ;
			Property(p => p.ViewTemplate).HasColumnName("post_controller").HasMaxLength(128) ;
			Property(p => p.ViewArchiveTemplate).HasColumnName("post_archive_controller").HasMaxLength(128) ;
			Property(p => p.Created).HasColumnName("post_created") ;
			Property(p => p.Updated).HasColumnName("post_updated") ;
			Property(p => p.Published).HasColumnName("post_published") ;
			Property(p => p.LastPublished).HasColumnName("post_last_published") ;
			Property(p => p.LastModified).HasColumnName("post_last_modified") ;

			HasRequired(p => p.Template) ;
			HasRequired(p => p.Permalink) ;
			HasMany(p => p.Categories).WithMany().Map(pc => { 
				pc.ToTable("relation") ; 
				pc.MapLeftKey(new string[] { "relation_data_id", "relation_draft" }) ; 
				pc.MapRightKey("relation_related_id" ) ;
			}) ;
			HasMany(p => p.Properties).WithRequired().HasForeignKey(pr => new { pr.ParentId, pr.IsDraft }) ;
			HasMany(p => p.Extensions).WithRequired().HasForeignKey(e => new { e.ParentId, e.IsDraft }) ;
			HasMany(p => p.Comments).WithRequired().HasForeignKey(c => new { c.ParentId, c.ParentIsDraft }) ;
		}
	}
}
