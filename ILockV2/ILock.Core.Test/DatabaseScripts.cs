// <copyright file="DatabaseScripts.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ILock.Core.Test
{
    using ILock.Core.Data;
    using ILock.Core.Data.Entities;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The database scripts.
    /// </summary>
    internal class DatabaseScripts
    {
        private readonly ServiceProvider serviceProvider;
        private readonly ICryptoService cryptoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseScripts"/> class.
        /// </summary>
        /// <param name="bootstrap">The bootstrap.</param>
        public DatabaseScripts(DependencyBootstrap bootstrap)
        {
            this.serviceProvider = bootstrap.ServiceProvider;
            this.cryptoService = this.serviceProvider.GetRequiredService<ICryptoService>();
        }

        /// <summary>
        /// Inserts the users.
        /// </summary>
        public void InsertUsers()
        {
            using (var context = this.serviceProvider.GetService<AuthDBContext>())
            {
                List<User> users = new List<User>
                {
                    new User()
                    {
                        Username = "umer123",
                        Email = "u@f.com",
                        Password = this.cryptoService.ComputePassword("umer123", "Nrm123"),
                    },
                    new User()
                    {
                        Username = "uzair123",
                        Email = "u@g.com",
                        Password = this.cryptoService.ComputePassword("uzair123", "Nrm123")
                    }
                };

                context!.AddRange(users);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Inserts the permissions.
        /// </summary>
        public void InsertPermissions()
        {
            using (var context = this.serviceProvider.GetService<AuthDBContext>())
            {
                List<Permission> permissions = new List<Permission>
                {
                    new Permission()
                    {
                        FeatureID = 1,
                        Access = "Full",
                        Type = "Feature"
                    },
                    new Permission()
                    {
                        FeatureID = 1,
                        Access = "Read",
                        Type = "Feature"
                    },
                    new Permission()
                    {
                        FeatureID = 2,
                        Access = "Full",
                        Type = "Feature"
                    },
                    new Permission()
                    {
                        FeatureID = 2,
                        Access = "None",
                        Type = "Feature"
                    },
                    new Permission()
                    {
                        FeatureID = 3,
                        Access = "Full",
                        Type = "Feature"
                    },
                    new Permission()
                    {
                        FeatureID = 3,
                        Access = "Full",
                        Type = "Feature"
                    },
                };

                context!.AddRange(permissions);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Insers the roles.
        /// </summary>
        public void InserRoles()
        {
            using (var context = this.serviceProvider.GetService<AuthDBContext>())
            {
                List<Role> roles = new List<Role>
                {
                    new Role()
                    {
                        Name = "Super-Admin",
                    },
                    new Role()
                    {
                        Name = "Ri-user",
                    }
                };

                context!.AddRange(roles);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Inserts the features.
        /// </summary>
        public void InsertFeatures()
        {
            using (var context = this.serviceProvider.GetService<AuthDBContext>())
            {
                //List<Feature> features = new List<Feature>
                //{
                //    new Feature()
                //    {
                //        Name = "Event",
                //        Type = "Module",
                //        FeatureAssociations = new(),
                //    },
                //    new Feature()
                //    {
                //        Name = "Financials",
                //        Type = "Module",
                //        FeatureAttributes = new()
                //    },
                //    new Feature()
                //    {
                //        Name = "Forecast",
                //        Type = "Module",
                //        FeatureAttributes = new()
                //    },
                //    new Feature()
                //    {
                //        Name = "Report",
                //        Type = "Module",
                //        FeatureAttributes = new()
                //    },
                //    new Feature()
                //    {
                //        Name = "Scenarios",
                //        Type = "Module",
                //        FeatureAttributes = new()
                //    },
                //    new Feature()
                //    {
                //        Name = "TFM",
                //        Type = "Module",
                //        FeatureAttributes = new()
                //    },
                //    new Feature()
                //    {
                //        Name = "CBP",
                //        Type = "Module",
                //        FeatureAttributes = new()
                //    }
                //};

                //context!.AddRange(features);
                //context.SaveChanges();
            }
        }

        /// <summary>
        /// Inserts the feature associations.
        /// </summary>
        public void InsertFeatureAssociations()
        {
            using (var context = this.serviceProvider.GetService<AuthDBContext>())
            {
                List<FeatureAssociation> featureAssociations = new List<FeatureAssociation>
                {
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "Russia",
                        ExternalID = 1,
                        FeatureID = 1,
                        AccessType = "Full"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "Russia",
                        ExternalID = 1,
                        FeatureID = 2,
                        AccessType = "Full"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "Russia",
                        ExternalID = 1,
                        FeatureID = 3,
                        AccessType = "Full"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "Russia",
                        ExternalID = 1,
                        FeatureID = 4,
                        AccessType = "Full"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "Russia",
                        ExternalID = 1,
                        FeatureID = 5,
                        AccessType = "Full"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "France",
                        ExternalID = 2,
                        FeatureID = 1,
                        AccessType = "Read"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "France",
                        ExternalID = 2,
                        FeatureID = 2,
                        AccessType = "Full"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "France",
                        ExternalID = 2,
                        FeatureID = 3,
                        AccessType = "Full"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "France",
                        ExternalID = 2,
                        FeatureID = 4,
                        AccessType = "Full"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "France",
                        ExternalID = 2,
                        FeatureID = 5,
                        AccessType = "None"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "UK",
                        ExternalID = 3,
                        FeatureID = 1,
                        AccessType = "None"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "UK",
                        ExternalID = 3,
                        FeatureID = 2,
                        AccessType = "Read"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "UK",
                        ExternalID = 3,
                        FeatureID = 3,
                        AccessType = "Read"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "UK",
                        ExternalID = 3,
                        FeatureID = 4,
                        AccessType = "Read"
                    },
                    new FeatureAssociation()
                    {
                        Key = "Country",
                        Value = "UK",
                        ExternalID = 3,
                        FeatureID = 5,
                        AccessType = "Read"
                    },
                };

                context!.AddRange(featureAssociations);
                context.SaveChanges();
            }
        }
    }
}
