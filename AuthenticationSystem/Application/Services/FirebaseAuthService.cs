using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AuthenticationSystem.Application.Services
{
    public class FirebaseAuthService
    {
        private static bool _initialized = false;
        private static readonly object _lock = new object();

        public FirebaseAuthService()
        {
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            if (!_initialized)
            {
                lock (_lock)
                {
                    if (!_initialized)
                    {
                        try
                        {
                            // Try multiple possible paths for the service account key
                            var possiblePaths = new[]
                            {
                                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "config", "serviceAccountKey.json"),
                                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Config", "serviceAccountKey.json"),
                                Path.Combine(Directory.GetCurrentDirectory(), "serviceAccountKey.json"),
                                "wwwroot/config/serviceAccountKey.json",
                                "wwwroot/Config/serviceAccountKey.json"
                            };

                            string? serviceAccountPath = null;
                            foreach (var path in possiblePaths)
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    serviceAccountPath = path;
                                    break;
                                }
                            }

                            if (serviceAccountPath != null)
                            {
                                FirebaseApp.Create(new AppOptions
                                {
                                    Credential = GoogleCredential.FromFile(serviceAccountPath)
                                });
                                _initialized = true;
                                Log.Information("Firebase Admin SDK initialized successfully from: {Path}", serviceAccountPath);
                            }
                            else
                            {
                                Log.Warning("Firebase service account key file not found. Tried paths: {Paths}", string.Join(", ", possiblePaths));
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "Failed to initialize Firebase Admin SDK");
                        }
                    }
                }
            }
        }

        public async Task<FirebaseToken?> VerifyTokenAsync(string idToken)
        {
            try
            {
                if (!_initialized)
                {
                    Log.Error("Firebase Admin SDK not initialized. Cannot verify token.");
                    return null;
                }

                var decoded = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                return decoded;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to verify Firebase token");
                return null;
            }
        }
    }
}

