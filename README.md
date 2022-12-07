# Microservices solution

**At the time of writing C# 7 is not yet being released.**

This repository contains several .Net 6 projects demonstrating the use of microservices with a dockerized SQL Server and Redis instance, as well as Prometheus integration for later consumption using Grafana or other solutions.

## Features: 

**Customer microservice**:
 * customer controller endpoint (Sql Server backend)
 * cache controller endpoint (Redis)

**Product microservice**:
 * Product controller endpoint (Sql Server backend)

**Docker compose project**:
 * Two microservices
 * Redis instance
 * SQL Server Instance
 

