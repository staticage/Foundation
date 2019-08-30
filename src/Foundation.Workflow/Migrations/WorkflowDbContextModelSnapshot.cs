﻿// <auto-generated />
using System;
using Foundation.Workflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Foundation.Workflow.Migrations
{
    [DbContext(typeof(WorkflowDbContext))]
    partial class WorkflowDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Foundation.Workflow.ExecutionPointer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("EndTime");

                    b.Property<string>("EventKey");

                    b.Property<string>("EventName");

                    b.Property<string>("PublishedEvents");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.Property<string>("StepId");

                    b.Property<Guid?>("WorkflowInstanceId");

                    b.HasKey("Id");

                    b.HasIndex("WorkflowInstanceId");

                    b.ToTable("ExecutionPointers");
                });

            modelBuilder.Entity("Foundation.Workflow.WorkflowInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("EndTime");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.Property<int>("Version");

                    b.Property<string>("WorkflowDefinitionName");

                    b.HasKey("Id");

                    b.ToTable("Workflows");
                });

            modelBuilder.Entity("Foundation.Workflow.ExecutionPointer", b =>
                {
                    b.HasOne("Foundation.Workflow.WorkflowInstance")
                        .WithMany("ExecutionPointers")
                        .HasForeignKey("WorkflowInstanceId");
                });
#pragma warning restore 612, 618
        }
    }
}
