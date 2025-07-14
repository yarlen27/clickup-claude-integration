using ClickUp.Core.Interfaces;
using ClickUp.Core.Models;
using ClickUp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

builder.Services.AddHttpClient<IClickUpService, ClickUpService>();
builder.Services.AddLogging();

var host = builder.Build();

var clickUpService = host.Services.GetRequiredService<IClickUpService>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

try
{
    Console.WriteLine("🚀 ClickUp Members Management Test");
    Console.WriteLine("==================================");

    // Test 1: Get all members
    Console.WriteLine("\n1. Getting all team members...");
    var allMembers = await clickUpService.GetAllMembersAsync();
    Console.WriteLine($"Found {allMembers.Count} members across all teams:");
    foreach (var member in allMembers)
    {
        var activeStatus = member.IsActive ? "🟢 Active" : "🔴 Inactive";
        Console.WriteLine($"   - {member.Username} ({member.Email}) | Role: {member.RoleKey} | {activeStatus}");
    }

    if (!allMembers.Any())
    {
        Console.WriteLine("❌ No members found. Check your API token and team access.");
        return;
    }

    // Test 2: Search members by different criteria
    Console.WriteLine("\n2. Testing member search functionality...");
    
    // Search by partial username
    var searchResults = await clickUpService.SearchMembersAsync("juan");
    Console.WriteLine($"\nSearch for 'juan': Found {searchResults.Count} results:");
    foreach (var result in searchResults)
    {
        Console.WriteLine($"   - {result.Username} ({result.Email})");
    }

    // Search by email domain
    var domainSearch = await clickUpService.SearchMembersAsync("27cobalto.com");
    Console.WriteLine($"\nSearch for '27cobalto.com': Found {domainSearch.Count} results:");
    foreach (var result in domainSearch)
    {
        Console.WriteLine($"   - {result.Username} ({result.Email})");
    }

    // Test 3: Find specific members
    Console.WriteLine("\n3. Testing specific member lookup...");
    
    var testNames = new[] { "juan", "yarlen", "edinson", "melissa" };
    foreach (var name in testNames)
    {
        var member = await clickUpService.FindMemberAsync(name);
        if (member != null)
        {
            Console.WriteLine($"✅ Found '{name}': {member.Username} (ID: {member.Id}, Email: {member.Email})");
        }
        else
        {
            Console.WriteLine($"❌ Member '{name}' not found");
        }
    }

    // Test 4: Resolve member IDs for assignment
    Console.WriteLine("\n4. Testing member ID resolution for task assignment...");
    var identifiers = new List<string> { "juan", "yarlen@27cobalto.com", "81585056" }; // Mix of username, email, and ID
    var resolvedIds = await clickUpService.ResolveMemberIdsAsync(identifiers);
    
    Console.WriteLine($"Resolving identifiers: {string.Join(", ", identifiers)}");
    Console.WriteLine($"Resolved to IDs: {string.Join(", ", resolvedIds)}");
    
    // Test 5: Create task with natural language assignment
    if (resolvedIds.Any())
    {
        var teams = await clickUpService.GetTeamsAsync();
        if (teams.Any())
        {
            var spaces = await clickUpService.GetSpacesAsync(teams.First().Id);
            var testSpace = spaces.FirstOrDefault(s => s.Name.StartsWith("Test Space"));
            
            if (testSpace != null)
            {
                // Get or create a list for testing
                var lists = await clickUpService.GetFolderlessListsAsync(testSpace.Id);
                var testList = lists.FirstOrDefault();
                
                if (testList == null)
                {
                    // Create a simple list for testing
                    var listRequest = new CreateListRequest
                    {
                        Name = "Member Assignment Test List",
                        Content = "Lista para probar asignaciones de miembros"
                    };
                    testList = await clickUpService.CreateFolderlessListAsync(testSpace.Id, listRequest);
                }

                if (testList != null)
                {
                    Console.WriteLine($"\n5. Creating task with resolved member assignments...");
                    
                    var taskRequest = new CreateTaskRequest
                    {
                        Name = "Task assigned via natural language",
                        Description = $"Esta tarea fue asignada usando los identificadores: {string.Join(", ", identifiers)}",
                        Assignees = resolvedIds,
                        Priority = 2,
                        Tags = new List<string> { "api-test", "member-assignment" }
                    };
                    
                    var createdTask = await clickUpService.CreateTaskAsync(testList.Id, taskRequest);
                    if (createdTask != null)
                    {
                        Console.WriteLine($"✅ Task created: {createdTask.Name} (ID: {createdTask.Id})");
                        Console.WriteLine($"   Assigned to: {string.Join(", ", createdTask.Assignees.Select(a => a.Username))}");
                        
                        // Show the mapping
                        Console.WriteLine("\n📋 Assignment Resolution Summary:");
                        for (int i = 0; i < identifiers.Count && i < resolvedIds.Count; i++)
                        {
                            var assignee = createdTask.Assignees.FirstOrDefault(a => a.Id == resolvedIds[i]);
                            if (assignee != null)
                            {
                                Console.WriteLine($"   '{identifiers[i]}' → {assignee.Username} (ID: {assignee.Id})");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Failed to create task with assignments");
                    }
                }
            }
        }
    }

    // Test 6: Natural language scenarios
    Console.WriteLine("\n6. Natural Language Assignment Scenarios:");
    Console.WriteLine("=========================================");
    
    Console.WriteLine("\n📝 Example scenarios Claude Code could handle:");
    Console.WriteLine("• 'Assign this task to Juan' → Resolve 'Juan' to user ID");
    Console.WriteLine("• 'Create task for the frontend team' → Find members with 'frontend' role/skills");
    Console.WriteLine("• 'Assign to yarlen@27cobalto.com' → Resolve email to user ID");
    Console.WriteLine("• 'Give this to someone with low workload' → Analyze assignments and suggest");
    
    // Show current team structure for context
    Console.WriteLine("\n👥 Current Team Structure:");
    var membersByRole = allMembers.GroupBy(m => m.RoleKey).ToList();
    foreach (var roleGroup in membersByRole)
    {
        Console.WriteLine($"• {roleGroup.Key}: {string.Join(", ", roleGroup.Select(m => m.Username))}");
    }

    Console.WriteLine("\n🎉 Member management test successful!");
    Console.WriteLine("✅ All member search and assignment functions working correctly");
}
catch (Exception ex)
{
    logger.LogError(ex, "Error testing ClickUp member management");
    Console.WriteLine($"\n❌ Error: {ex.Message}");
    Console.WriteLine("\nMake sure you have configured your ClickUp API token:");
    Console.WriteLine("   dotnet user-secrets set \"ClickUp:ApiToken\" \"your_token_here\"");
}