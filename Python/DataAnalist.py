import matplotlib.pyplot as pl
import numpy as np
import copy
import math

mypath =  "C:/Users/Asus/Documents/Tecnico/Cadeiras2Sem/ASMA/Project/MultiagentEcosystem/MultiAgentEcosystem"

class Environment:
    average_stats = {
        "day" : 0,
        "agents" : 0,
        "food" : 0
    }
    
    count = 0
    def __init__(self, statsLine):
        Environment.count += 1
        statsLine = statsLine[:-1]
        self.statsList = statsLine.split(" ")
        
        self.stats = {
            "day" : int(self.statsList[1]),
            "agents" : int(self.statsList[0]),
            "food" : int(self.statsList[2])
        }
        
        for key in Environment.average_stats:
            Environment.average_stats[key] = update_avg(self.stats[key], Environment.average_stats[key], Environment.count)

class Obituary:
    average_stats = {
            "lifespan"              : 0,
            "babiesPerAgent"        : 0,
            "speed"                 : 0,
            "vitality"              : 0,
            "starvingDamage"        : 0,
            "wanderRate"            : 0,
            "smellToSenseRatio"     : 0,
            "strength"              : 0,
            "intimidationFactor"    : 0,
            "perceptionAccuracy"    : 0,
            "procreateModifier"     : 0,
            "attackModifier"        : 0,
            "minHunger"             : 0
    }
    
    count = 0
    def __init__(self, statsLine):
        statsLine = statsLine.replace(",", ".")
        self.statsList = statsLine.split(" ")

        Obituary.count += 1
        
        self.stats ={
            "lifespan"              : float(self.statsList[0]),
            "babiesPerAgent"        : float(self.statsList[1]),
            "speed"                 : float(self.statsList[2]),
            "vitality"              : float(self.statsList[3]),
            "starvingDamage"        : float(self.statsList[4]),
            "wanderRate"            : float(self.statsList[5]),
            "smellToSenseRatio"     : float(self.statsList[6]),
            "strength"              : float(self.statsList[7]),
            "intimidationFactor"    : float(self.statsList[8]),
            "perceptionAccuracy"    : float(self.statsList[9]),
            "procreateModifier"     : float(self.statsList[10]),
            "attackModifier"        : float(self.statsList[11]),
            "minHunger"             : float(self.statsList[12])
        }
            
        for key in Obituary.average_stats:
            Obituary.average_stats[key] = update_avg(self.stats[key], Obituary.average_stats[key], Obituary.count)

class Genome:
    
    count = 0
    def __init__(self, statsLine):
        Genome.count += 1
        statsLine = statsLine.replace(",", ".")
        self.statsList = statsLine.split(" ")
        print(self.statsList)
        self.stats ={
            "speed"                 : float(self.statsList[0]),
            "vitality"              : float(self.statsList[1]),
            "starvingDamage"        : float(self.statsList[2]),
            "wanderRate"            : float(self.statsList[3]),
            "smellToSenseRatio"     : float(self.statsList[4]),
            "strength"              : float(self.statsList[5]),
            "intimidationFactor"    : float(self.statsList[6]),
            "perceptionAccuracy"    : float(self.statsList[7]),
            "procreateModifier"     : float(self.statsList[8]),
            "attackModifier"        : float(self.statsList[9]),
            "minHunger"             : float(self.statsList[10])
        }
            
        for key in average_stats_aux:
            average_stats_aux[key] = update_avg(self.stats[key], average_stats_aux[key], Genome.count)
            standard_deviations_aux[key] = update_sd(self.stats[key], average_stats_aux[key], standard_deviations_aux[key], Genome.count)
            

def update_avg(sample, current_average, sample_count):
    return current_average + (sample - current_average)/sample_count

def update_sd(sample, current_average, current_sd, sample_count):
    if(sample_count == 1):
        return 0
    return ((sample_count - 2) / (sample_count - 1)) * current_sd + (1 / sample_count) * pow((sample - current_average), 2)

def printGraph(x, y, x_label, y_label, graph_name, graph_number) :
    fig, envPlot = pl.subplots()
    envPlot.plot(x, y,'--gD')

    envPlot.set(xlabel=x_label, ylabel=y_label,
        title=graph_name)

    envPlot.grid()

    path = mypath + "/Python/Graph{s}.png".format(s=graph_number)

    fig.savefig(path) 

    envPlot.grid()
    pl.show()

def printGraphWithSd(x, y, x_label, y_label, graph_name, graph_number, sd) :
    fig, envPlot = pl.subplots()
    envPlot.plot(x, y,'--gD')
    
    y_np = np.array(y)
    sd_np = np.array(sd)

    envPlot.set(xlabel=x_label, ylabel=y_label,
        title=graph_name)
    
    envPlot.fill_between(x, y_np+np.sqrt(sd_np), y_np-np.sqrt(sd_np), facecolor='blue', alpha=0.5)
    
    envPlot.grid()

    path = mypath +  "/Python/Graph{s}.png".format(s=graph_number)

    fig.savefig(path) 

    envPlot.grid()
    pl.show()
    
def printBarGraph(x, y, x_label, y_label, graph_name, graph_number) :
    fig, envPlot = pl.subplots()
    width = 0.4
    l = np.arange(len(x))

    rects1 = envPlot.bar(l, y, width, label=y_label)

    envPlot.set(xlabel=x_label, ylabel=y_label, title=graph_name)
    envPlot.set_xticks(l)
    envPlot.set_xticklabels(x)
    envPlot.legend()

    path = mypath  + "/Python/Graph{s}.png".format(s=graph_number)

    fig.savefig(path) 

    envPlot.bar_label(rects1, padding=3)
    pl.show()
    
def printScatterPlot(x, y, x_label, y_label, graph_name, fig_name, xlim_min, ylim_min, xlim_max, ylim_max) :
    fig, plt = pl.subplots()
    plt.axis(xmin=xlim_min, xmax=xlim_max, ymin=ylim_min, ymax=ylim_max)

    plt.plot(x, y,'o')

    plt.set(xlabel=x_label, ylabel=y_label,
        title=graph_name)
    plt.grid()

    path = mypath + "/Data/{s}.png".format(s=fig_name)

    fig.savefig(path) 

    plt.grid()
    pl.show()

#### ENVIRONMENT
f = open( mypath + "/Data/gamesave.txt", "r") 

gamedata = f.readlines()

data = {
        "day" : [],
        "agents" : [],
        "food" : []
    }

for i in range(1, len(gamedata)):
    environment = Environment(gamedata[i])
    for key in data:
        data[key].append(environment.stats[key])


printGraph(data["day"], data["agents"], 'days', 'agents', 'Number of agents per day', 1)

printGraph(data["day"], data["food"], 'days', 'food', 'Number of food per day', 2)

f.close

### OBITUARY

f = open( mypath + "/Data/obituarySave.txt", "r") 

obituaryData = f.readlines()
obituaries = []
for i in range(len(obituaryData)):
    if(obituaryData[i][0] != '-'):
        obituary = Obituary(obituaryData[i])
        obituaries.append(obituary)

for key in ["speed","vitality","starvingDamage","wanderRate","smellToSenseRatio" ,"strength","intimidationFactor","perceptionAccuracy","procreateModifier" ,"attackModifier","minHunger"]:
    x = []
    y = []
    for ob in obituaries:
        x.append(ob.stats["lifespan"]/24.0)
        y.append(ob.stats[key])
    printScatterPlot(x,y,"lifespan",key,key + " by lifespan",key + " by lifespan", min(x), min(y), max(x),max(y))
for key in ["speed","vitality","starvingDamage","wanderRate","smellToSenseRatio" ,"strength","intimidationFactor","perceptionAccuracy","procreateModifier" ,"attackModifier","minHunger"]:
    x = []
    y = []
    for ob in obituaries:
        x.append(ob.stats["babiesPerAgent"])
        y.append(ob.stats[key])
    printScatterPlot(x,y,"children",key,key + " by children",key + " by children", min(x), min(y), max(x),max(y))
print(obituary.count)
    
print("Average Lifespan: ", obituary.average_stats["lifespan"] / 24, "days.")
print("Average Babies per Agent: ", obituary.average_stats["babiesPerAgent"], "days.")

f.close

### GENOME
f = open( mypath + "/Data/genomeSave.txt", "r") 

data_genome = {
        "speed"                 : [],
        "vitality"              : [],
        "starvingDamage"        : [],
        "wanderRate"            : [],
        "smellToSenseRatio"     : [],
        "strength"              : [],
        "intimidationFactor"    : [],
        "perceptionAccuracy"    : [],
        "procreateModifier"     : [],
        "attackModifier"        : [],
        "minHunger"             : []
    }

average_stats ={
        "speed"                 : [],
        "vitality"              : [],
        "starvingDamage"        : [],
        "wanderRate"            : [],
        "smellToSenseRatio"     : [],
        "strength"              : [],
        "intimidationFactor"    : [],
        "perceptionAccuracy"    : [],
        "procreateModifier"     : [],
        "attackModifier"        : [],
        "minHunger"             : []
    }

standard_deviations ={
        "speed"                 : [],
        "vitality"              : [],
        "starvingDamage"        : [],
        "wanderRate"            : [],
        "smellToSenseRatio"     : [],
        "strength"              : [],
        "intimidationFactor"    : [],
        "perceptionAccuracy"    : [],
        "procreateModifier"     : [],
        "attackModifier"        : [],
        "minHunger"             : []
    }

data_aux ={
        "speed"                 : [],
        "vitality"              : [],
        "starvingDamage"        : [],
        "wanderRate"            : [],
        "smellToSenseRatio"     : [],
        "strength"              : [],
        "intimidationFactor"    : [],
        "perceptionAccuracy"    : [],
        "procreateModifier"     : [],
        "attackModifier"        : [],
        "minHunger"             : []
    }

average_stats_aux ={
        "speed"                 : 0,
        "vitality"              : 0,
        "starvingDamage"        : 0,
        "wanderRate"            : 0,
        "smellToSenseRatio"     : 0,
        "strength"              : 0,
        "intimidationFactor"    : 0,
        "perceptionAccuracy"    : 0,
        "procreateModifier"     : 0,
        "attackModifier"        : 0,
        "minHunger"             : 0
    }
    
standard_deviations_aux = {
    "speed"                 : 0,
    "vitality"              : 0,
    "starvingDamage"        : 0,
    "wanderRate"            : 0,
    "smellToSenseRatio"     : 0,
    "strength"              : 0,
    "intimidationFactor"    : 0,
    "perceptionAccuracy"    : 0,
    "procreateModifier"     : 0,
    "attackModifier"        : 0,
    "minHunger"             : 0
}
genomeList = []



for i in range(len(data["day"])):        
    for ag in range(len(data["agents"])):
        genome = Genome(f.readline())
        genomeList.append(genome)
        
        for key in data_aux:
            data_aux[key].append(genome.stats[key])
    
    for key in data_genome:
        average_stats[key].append(average_stats_aux[key])
        average_stats_aux[key] = 0
        standard_deviations[key].append(standard_deviations_aux[key])
        standard_deviations_aux[key] = 0
        Genome.count = 0
        data_genome[key].append(copy.copy(data_aux[key]))
        data_aux[key].clear()
        
for key in data_genome:
    printGraphWithSd(data["day"], average_stats[key], 'days', 'average {s}'.format(s=key), 'Average {s} per day'.format(s=key), key, standard_deviations[key])
    #printGraph(data["day"], average_stats[key], 'days', 'average {s}'.format(s=key), 'Average {s} per day'.format(s=key), key)

#procreate modifier and attack modifier
printScatterPlot(data_genome["procreateModifier"][0], data_genome["attackModifier"][0], 'Procreate Modifier', 'Attack Modifier', 'procreate modifier and attack modifier comparision (Day {s})'.format(s=1), 'proc_att_{s}'.format(s=i), 0.5, 0.5, 1.5, 1.5)

for i in [10, 20, 30, 40, 99]:
    printScatterPlot(data_genome["procreateModifier"][i-1], data_genome["attackModifier"][i-1], 'Procreate Modifier', 'Attack Modifier', 'procreate modifier and attack modifier comparision (Day {s})'.format(s=i), 'proc_att_{s}'.format(s=i), 0.5, 0.5, 1.5, 1.5)

for i in [10, 20, 30, 40, 99]:
    printScatterPlot(data_genome["speed"][i-1], data_genome["vitality"][i-1], 'speed', 'vitality', 'speed and vitality comparision (Day {s})'.format(s=i), 'proc_att_{s}'.format(s=i), 5, 5, 20, 20)


f.close
