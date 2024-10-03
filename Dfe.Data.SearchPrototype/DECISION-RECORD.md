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