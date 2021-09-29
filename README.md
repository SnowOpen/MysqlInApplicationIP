# 基于Azure Mysql和Redis的应用开发经验积累

## 资产积累
- Application Resilience Enhancement.pptx (基于MySql/Redis的现代化应用的Tech IP)
- Azure Function App (BlobTrigger & QueueTrigger Function 从storage Account中读取数据后推送至Azure Log Analytics)
- spring-boot-redisson-failover-sdk (客户端同时接入两个Redis集群时，使用此jar包进行自动故障转移)

## 常见的Mysql/Redis应用开发的问题


- Connection Management
	- Timeout
	- Unexpected disconnection
	- Connection exception
	- Bad parameters
	- Connection pool
	- Connection mode
	- Keep-Alive
	- Proxy
	- SSL/Security
	- Version compatible
- Reliability
	- Deadlock
	- Slow Query
	- Bad Index
	- Bad Code
	- Bad expire mode
	- Exception handling
	- Memory leak
	- Cache penetration
	- Cache avalanche
- Availability 
	- Monitor
	- Failover
	- High Availably
	- Active-Active 
	- Disaster Recovery 


## 目前积累的优化能力

- Reliability
	- Connection Pool Management
		- Use SDK/Library
		- Client & server configuration check & optimization
		- Package version compatibility check & handling		
	- Exception Management
		- Exception capture
		- Exception handling
		- Exception alarm & report		
	- Memory Management
		- Memory leak check & resolve
		- Cache design & usage suggestions
		- Cache penetration / avalanche check & resolve		
	- Performance Optimize 
		- Slow Query check & resolve
		- Index optimization
		- Deadlock check & resolve
		- Code Health Check & Optimization
		- Performance test
- Availability
	- Issues & Bugs troubleshooting	
	- Automatic  scale up & scale down	
	- Design & implementation of Proxy	
	- Design & evaluation of high availability application	
	- Design & implementation of Manual Failover	
	- Design & implementation of Automatic Failover	
	- Design & implementation of Disaster Recover	
	- Design & implementation of Active-Active
- Monitoring
	- Use Azure Monitor tool	
	- Use Auto-alarm	
	- Change Alarm baseline	
	- Logs collection and analysis	
	- Metrics collection and analysis	
	- Use custom metrics	
	- Use Azure dashboard
	
