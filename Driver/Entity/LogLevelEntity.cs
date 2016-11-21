/*
* DISCLAIMER
*
* Copyright 2016 ArangoDB GmbH, Cologne, Germany
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
* Copyright holder is ArangoDB GmbH, Cologne, Germany
*/

namespace ArangoDB.Entity
{
    /// <author>Mark - mark at arangodb.com</author>
    public class LogLevelEntity
    {
        public enum LogLevel
        {
            FATAL,
            ERROR,
            WARNING,
            INFO,
            DEBUG,
            TRACE,
            DEFAULT
        }

        private LogLevelEntity.LogLevel agency;

        private LogLevelEntity.LogLevel agencycomm;

        private LogLevelEntity.LogLevel cluster;

        private LogLevelEntity.LogLevel collector;

        private LogLevelEntity.LogLevel communication;

        private LogLevelEntity.LogLevel compactor;

        private LogLevelEntity.LogLevel config;

        private LogLevelEntity.LogLevel datafiles;

        private LogLevelEntity.LogLevel graphs;

        private LogLevelEntity.LogLevel heartbeat;

        private LogLevelEntity.LogLevel mmap;

        private LogLevelEntity.LogLevel performance;

        private LogLevelEntity.LogLevel queries;

        private LogLevelEntity.LogLevel replication;

        private LogLevelEntity.LogLevel requests;

        private LogLevelEntity.LogLevel startup;

        private LogLevelEntity.LogLevel threads;

        private LogLevelEntity.LogLevel v8;

        public LogLevelEntity()
            : base()
        {
        }

        public virtual LogLevelEntity.LogLevel getAgency()
        {
            return this.agency;
        }

        public virtual void setAgency(LogLevelEntity.LogLevel agency)
        {
            this.agency = agency;
        }

        public virtual LogLevelEntity.LogLevel getAgencycomm()
        {
            return this.agencycomm;
        }

        public virtual void setAgencycomm(LogLevelEntity.LogLevel agencycomm
            )
        {
            this.agencycomm = agencycomm;
        }

        public virtual LogLevelEntity.LogLevel getCluster()
        {
            return this.cluster;
        }

        public virtual void setCluster(LogLevelEntity.LogLevel cluster
            )
        {
            this.cluster = cluster;
        }

        public virtual LogLevelEntity.LogLevel getCollector()
        {
            return this.collector;
        }

        public virtual void setCollector(LogLevelEntity.LogLevel collector
            )
        {
            this.collector = collector;
        }

        public virtual LogLevelEntity.LogLevel getCommunication()
        {
            return this.communication;
        }

        public virtual void setCommunication(LogLevelEntity.LogLevel
            communication)
        {
            this.communication = communication;
        }

        public virtual LogLevelEntity.LogLevel getCompactor()
        {
            return this.compactor;
        }

        public virtual void setCompactor(LogLevelEntity.LogLevel compactor
            )
        {
            this.compactor = compactor;
        }

        public virtual LogLevelEntity.LogLevel getConfig()
        {
            return this.config;
        }

        public virtual void setConfig(LogLevelEntity.LogLevel config)
        {
            this.config = config;
        }

        public virtual LogLevelEntity.LogLevel getDatafiles()
        {
            return this.datafiles;
        }

        public virtual void setDatafiles(LogLevelEntity.LogLevel datafiles
            )
        {
            this.datafiles = datafiles;
        }

        public virtual LogLevelEntity.LogLevel getGraphs()
        {
            return this.graphs;
        }

        public virtual void setGraphs(LogLevelEntity.LogLevel graphs)
        {
            this.graphs = graphs;
        }

        public virtual LogLevelEntity.LogLevel getHeartbeat()
        {
            return this.heartbeat;
        }

        public virtual void setHeartbeat(LogLevelEntity.LogLevel heartbeat
            )
        {
            this.heartbeat = heartbeat;
        }

        public virtual LogLevelEntity.LogLevel getMmap()
        {
            return this.mmap;
        }

        public virtual void setMmap(LogLevelEntity.LogLevel mmap)
        {
            this.mmap = mmap;
        }

        public virtual LogLevelEntity.LogLevel getPerformance()
        {
            return this.performance;
        }

        public virtual void setPerformance(LogLevelEntity.LogLevel performance
            )
        {
            this.performance = performance;
        }

        public virtual LogLevelEntity.LogLevel getQueries()
        {
            return this.queries;
        }

        public virtual void setQueries(LogLevelEntity.LogLevel queries
            )
        {
            this.queries = queries;
        }

        public virtual LogLevelEntity.LogLevel getReplication()
        {
            return this.replication;
        }

        public virtual void setReplication(LogLevelEntity.LogLevel replication
            )
        {
            this.replication = replication;
        }

        public virtual LogLevelEntity.LogLevel getRequests()
        {
            return this.requests;
        }

        public virtual void setRequests(LogLevelEntity.LogLevel requests
            )
        {
            this.requests = requests;
        }

        public virtual LogLevelEntity.LogLevel getStartup()
        {
            return this.startup;
        }

        public virtual void setStartup(LogLevelEntity.LogLevel startup
            )
        {
            this.startup = startup;
        }

        public virtual LogLevelEntity.LogLevel getThreads()
        {
            return this.threads;
        }

        public virtual void setThreads(LogLevelEntity.LogLevel threads
            )
        {
            this.threads = threads;
        }

        public virtual LogLevelEntity.LogLevel getV8()
        {
            return this.v8;
        }

        public virtual void setV8(LogLevelEntity.LogLevel v8)
        {
            this.v8 = v8;
        }
    }
}