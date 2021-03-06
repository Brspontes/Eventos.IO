﻿using Eventos.IO.Domain.ORGANIZADORES;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Infra.Data.EXTENSIONS
{
    public class OrganizadorMapping : EntityTypeConfiguration<Organizador>
    {
        public override void Map(EntityTypeBuilder<Organizador> builder)
        {
            builder.Property(e => e.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.CPF)
                .HasColumnType("varchar(11)")
                .HasMaxLength(11)
                .IsRequired();

            builder
               .Ignore(e => e.ValidationResult);

            builder
                .Ignore(e => e.CascadeMode);

            builder
                .ToTable("Organizadores");
        }
    }
}
