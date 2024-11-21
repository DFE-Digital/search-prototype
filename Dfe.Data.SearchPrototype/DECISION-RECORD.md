---
status: Accepted
date: 27/09/2024 (Retrospectively)
decider's: Scott Dawson (initial requirement) 
---

# AD: Stakeholder requirement - Clean architecture (demonstrate principles).

## Context and Problem Statement

Develop establishment search system through the application of clean-architectural principles 

## Decision Drivers

* Desire to divide the overall system into manageable parts to reduce complexity
* Loose coupling/low friction - allow for exchange of system component parts without affecting others
* Ensure SOLID principles are followed and dependency inversion principle is clearly enforced 

## Considered Options

1. Single project and solution with clean tiers divided through name-space/folder structure 
2. Single solution with separate projects defining clean tiers
3. Separate solutions for core (application/domain) tier, and infrastructure, presentation tiers
4. The necessity for separate repositories (and associated pipelines) for each tier defined

## Decision Outcome

We decided to apply the core domain/application tier in a single project, as well as align a default search service implementation (leveraging Azure AI Search) in a different project under the same solution to simplify development.

We decided to leverage existing work undertaken during the CSCP re-write to create a common infrastructure library for handling specific Azure AI search concerns, which could then be utilised in the concrete search service adapter implementation. This was developed in a separate solution/project and repo (with associated CI/CD pipeline).


### Consequences

* Good, because the Layers pattern provides high flexibility regarding technology selections within the layers (changeability) and enables teams to work on system parts in parallel.
* Bad, because there might be a performance penalty for each level of indirection and some undesired replication of implementation artifacts. There may also be a steeper learning curve for some wishing to implement this or a similar approach.

## More Information

A follow-on decision may be required to assign logical layers to physical tiers.

---
status: Accepted
date: 27/09/2024 (Retrospectively)
decider's: Team 
---

# AD: Use of Azure AI Search

## Context and Problem Statement

Develop search system which allows establishments to be located via name, urn 

## Decision Drivers

Azure AI Search chosen given we had a reusable library (of sorts) and we
had some experience configuring.

* Desire to re-use existing patterns and approaches used to implement search functionality
* Loose coupling/low friction - allow for potential exchange of underlying search functionality 
* Ensure SOLID principles are followed and dependency inversion principle is clearly enforced 

## Considered Options

1. Decoupled search infrastructure tier with a demonstrable search specific implementation.
2. Use of Azure AI search.
3. Use of ElasticSearch.
4. SQL full text search.
5. Hybrid search option.


## Decision Outcome

We decided to utilise Azure AI Search given we had a reusable library (of sorts) and we
had some experience configuring gained from previous project (CSCP, GIAP). Other options were explored but it was decided that the project should not gravitate around search infrastructure, but rather should be a demonstrator for clean-architecture. As such, the quickest route to a fully integrated service was pusued.


### Consequences

* Good, because we were able to develop the search infrastructure piece rapidly and utilise existing components and patterns.
* Bad, because we weren't able to demonstrate other search implementations to explicitly highlight the flexibility of the architecture.

## More Information

Follow-on work may be required to demonstrate other search implementations, i.e. Eslastic-Search, etc

---
status: Accepted
date: 27/09/2024 (Retrospectively)
decider's: Team/Stakeholder 
---

# AD: Code in the open

## Context and Problem Statement

Develop search system as open source software as per government guidelines

## Decision Drivers

GitHub used as it is a government standard platform for version control, code deployments, authentication, CI, Dependabot and GitHub Pages

* Desire to align with government development standard
* Facilitates service assessment requirements

## Considered Options

1. GitHub: DFE-Digital


## Decision Outcome

Align to current DfE/governmental standards for open source development of the search service.


### Consequences

* Good, because we were able to develop the search infrastructure piece rapidly and utilise existing components and patterns.
* Bad, no obvious concerns. 

## More Information
None



What we did (This is the complete list of retrospective decisions to be applied in the above format)
----------------------------------------------------------------------------------------------------

Decided deploy to CIP (no Infrastructure as code - decision made outside of the team).
 
Hydrated AI search from GIAS daily extract (csv).
Pushed data to search index.
All search infrastructure provisioned manually.
Choose easiest and cheapest path to populating search infrastructure to allow search capabilities.
We considered pulling data but rejected due to additional infrastructure needs, lack of familiarity
of certain tech, and time-boxing.
 
Decided to fully leverage GitHub capabilities.
 
Repo split decision made due to:
	Peripheral Azure AI Search considered a re-usable infrastructure library - separate repo created
	Core Application tier (clean architecture demo) - separate repo created
	Presentation tier inc. web MVC and API (a throw-away example of a possible front-end) - separate repo created
 
Decision was made to consume NuGet packages generated from the infrastructure and appplication tier directly within the presentation tier(s)
as opposed to provisioning a network-based API - considered it added unwanted complexity. 
Front-end we decided to use MVC due to familiarity (as opposed to Razor pages).
Devs decided to take ownership through testing of front end view components.
 
Decision made to include comprehensive code comments with a view to generating mark-down (at some point),
including compiler rule to flag missing narrative.
 
 
What we didn't do
-----------------
We didn't look any further than AI Search capabilities.
 
No geo-location searching as yet.
 
 
Possible further R & D
----------------------
Elastic Search.














