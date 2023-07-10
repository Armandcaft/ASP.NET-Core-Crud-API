using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrudAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configure les services de l'application.
        /// </summary>
        /// <param name="services">Collection des services disponibles.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration des services (ajoutez vos services ici)
            
            services.AddControllers(); // Ajoute la prise en charge des contrôleurs

            services.AddMemoryCache();
            services.AddMvc();
        }

        /// <summary>
        /// Configure le pipeline de requête HTTP de l'application.
        /// </summary>
        /// <param name="app">Constructeur d'application.</param>
        /// <param name="env">Environnement d'hébergement.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Utilise une page d'exception détaillée en environnement de développement
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Redirige les exceptions vers une page d'erreur
                app.UseHsts(); // Utilise HTTP Strict Transport Security (HSTS) pour la sécurité
            }

            app.UseHttpsRedirection(); // Redirige les requêtes HTTP vers HTTPS
            app.UseStaticFiles(); // Autorise l'utilisation de fichiers statiques (par exemple, des images, des fichiers CSS, etc.)

            app.UseRouting(); // Gère le routage des requêtes

            app.UseAuthorization(); // Ajoute le middleware d'autorisation

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Définit les points de terminaison pour les contrôleurs
            });
        }
    }
}
