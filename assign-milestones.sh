#!/bin/bash

# ClickUp Integration - Assign Issues to Milestones/Sprints
# Assigns all 42 issues to their corresponding sprint milestones

echo "🎯 Assigning 42 issues to sprint milestones..."

# SPRINT 1: Foundation (Issues #1-8) - Milestone 1
echo "📦 Assigning Sprint 1: Foundation issues (#1-8)..."
gh issue edit 1 --milestone "Sprint 1: Foundation"
gh issue edit 2 --milestone "Sprint 1: Foundation" 
gh issue edit 3 --milestone "Sprint 1: Foundation"
gh issue edit 4 --milestone "Sprint 1: Foundation"
gh issue edit 5 --milestone "Sprint 1: Foundation"
gh issue edit 6 --milestone "Sprint 1: Foundation"
gh issue edit 7 --milestone "Sprint 1: Foundation"
gh issue edit 8 --milestone "Sprint 1: Foundation"

# SPRINT 2: Core Services (Issues #9-14) - Milestone 2
echo "🔌 Assigning Sprint 2: Core Services issues (#9-14)..."
gh issue edit 9 --milestone "Sprint 2: Core Services"
gh issue edit 10 --milestone "Sprint 2: Core Services"
gh issue edit 11 --milestone "Sprint 2: Core Services"
gh issue edit 12 --milestone "Sprint 2: Core Services"
gh issue edit 13 --milestone "Sprint 2: Core Services"
gh issue edit 14 --milestone "Sprint 2: Core Services"

# SPRINT 3: MCP Integration (Issues #15-19) - Milestone 3
echo "🛠️ Assigning Sprint 3: MCP Integration issues (#15-19)..."
gh issue edit 15 --milestone "Sprint 3: MCP Integration"
gh issue edit 16 --milestone "Sprint 3: MCP Integration"
gh issue edit 17 --milestone "Sprint 3: MCP Integration"
gh issue edit 18 --milestone "Sprint 3: MCP Integration"
gh issue edit 19 --milestone "Sprint 3: MCP Integration"

# SPRINT 4: Task Tools (Issues #20-25) - Milestone 4
echo "🎯 Assigning Sprint 4: Task Tools issues (#20-25)..."
gh issue edit 20 --milestone "Sprint 4: Task Tools"
gh issue edit 21 --milestone "Sprint 4: Task Tools"
gh issue edit 22 --milestone "Sprint 4: Task Tools"
gh issue edit 23 --milestone "Sprint 4: Task Tools"
gh issue edit 24 --milestone "Sprint 4: Task Tools"
gh issue edit 25 --milestone "Sprint 4: Task Tools"

# SPRINT 5: Member & Structure Tools (Issues #26-33) - Milestone 5
echo "👥 Assigning Sprint 5: Member & Structure Tools issues (#26-33)..."
gh issue edit 26 --milestone "Sprint 5: Member & Structure Tools"
gh issue edit 27 --milestone "Sprint 5: Member & Structure Tools"
gh issue edit 28 --milestone "Sprint 5: Member & Structure Tools"
gh issue edit 29 --milestone "Sprint 5: Member & Structure Tools"
gh issue edit 30 --milestone "Sprint 5: Member & Structure Tools"
gh issue edit 31 --milestone "Sprint 5: Member & Structure Tools"
gh issue edit 32 --milestone "Sprint 5: Member & Structure Tools"
gh issue edit 33 --milestone "Sprint 5: Member & Structure Tools"

# SPRINT 6: Analytics & Testing (Issues #34-42) - Milestone 6
echo "📊 Assigning Sprint 6: Analytics & Testing issues (#34-42)..."
gh issue edit 34 --milestone "Sprint 6: Analytics & Testing"
gh issue edit 35 --milestone "Sprint 6: Analytics & Testing"
gh issue edit 36 --milestone "Sprint 6: Analytics & Testing"
gh issue edit 37 --milestone "Sprint 6: Analytics & Testing"
gh issue edit 38 --milestone "Sprint 6: Analytics & Testing"
gh issue edit 39 --milestone "Sprint 6: Analytics & Testing"
gh issue edit 40 --milestone "Sprint 6: Analytics & Testing"
gh issue edit 41 --milestone "Sprint 6: Analytics & Testing"
gh issue edit 42 --milestone "Sprint 6: Analytics & Testing"

echo "✅ All 42 issues assigned to sprint milestones successfully!"
echo "🎯 Sprint organization complete:"
echo "   📦 Sprint 1: Issues #1-8 (Foundation)"
echo "   🔌 Sprint 2: Issues #9-14 (Core Services)" 
echo "   🛠️ Sprint 3: Issues #15-19 (MCP Integration)"
echo "   🎯 Sprint 4: Issues #20-25 (Task Tools)"
echo "   👥 Sprint 5: Issues #26-33 (Member & Structure Tools)"
echo "   📊 Sprint 6: Issues #34-42 (Analytics & Testing)"
echo ""
echo "🚀 Ready for sprint-based development!"