import matplotlib.pyplot as pl
import numpy as np


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
        print(self.statsList)
        self.stats = {
            "day" : int(self.statsList[1]),
            "agents" : int(self.statsList[0]),
            "food" : int(self.statsList[2])
        }
        for key in Environment.average_stats:
            Environment.average_stats[key] = update_avg(self.stats[key], Environment.average_stats[key], Environment.count)


class Genome:
    average_stats ={
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
        Genome.count += 1
        statsLine = statsLine.replace(",", ".")
        self.statsList = statsLine.split(" ")
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
        for key in Genome.average_stats:
            Genome.average_stats[key] = update_avg(self.stats[key], Genome.average_stats[key], Genome.count)


def update_avg(sample, current_average, sample_count):
    return current_average + (sample - current_average)/sample_count


#### ENVIRONMENT
f = open("C:/Users/nocas/Documents/MultiAgentEcosystem/Data/gamesave_firstexperiment.txt", "r") 

gamedata = f.readlines()

environmentData = []
environmentDataDays = []
environmentDataAgents = []
environmentDataFood = []



for i in range(1, len(gamedata)):
    environment = Environment(gamedata[i])
    environmentDataDays.append(environment.stats["day"])
    environmentDataAgents.append(environment.stats["agents"])
    environmentDataFood.append(environment.stats["food"])
    environmentData.append(environment)



def printGraph(x, y, x_label, y_label, graph_name, graph_number) :
    fig, envPlot = pl.subplots()
    envPlot.plot(x, y,'--gD')

    envPlot.set(xlabel=x_label, ylabel=y_label,
        title=graph_name)
    envPlot.grid()

    path = "C:/Users/nocas/Documents/MultiAgentEcosystem/Python/Graph{s}.png".format(s=graph_number)

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

    path = "C:/Users/nocas/Documents/MultiAgentEcosystem/Python/Graph{s}.png".format(s=graph_number)

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

    path = "C:/Users/nocas/Documents/MultiAgentEcosystem/Python/{s}.png".format(s=fig_name)

    fig.savefig(path) 

    plt.grid()
    pl.show()


printGraph(environmentDataDays, environmentDataAgents, 'days', 'agents', 'Number of agents per day', 1)

printGraph(environmentDataDays, environmentDataFood, 'days', 'food', 'Number of food per day', 2)

f.close

### GENOME
f = open("C:/Users/nocas/Documents/MultiAgentEcosystem/Data/genomesave_firstexperiment.txt", "r") 

genomeData = []

DataSpeed = []            
DataVitality = []            
DataStarvingDamage = []   
DataWanderRate = []          
DataSmellToSenseRatio = []   
DataStrength  = []
DataIntimidationFactor = []  
DataPerceptionAccuracy = []
DataProcreateModifier = []
DataAttackModifier = []
DataMinHunger = []  

AverageDataSpeed = []            
AverageDataVitality = []            
AverageDataStarvingDamage = []   
AverageDataWanderRate = []          
AverageDataSmellToSenseRatio = []   
AverageDataStrength  = []
AverageDataIntimidationFactor = []  
AverageDataPerceptionAccuracy = []
AverageDataProcreateModifier = []
AverageDataAttackModifier = []
AverageDataMinHunger = [] 


for i in environmentDataDays:
    environmentDataSpeed = []            
    environmentDataVitality = []            
    environmentDataStarvingDamage = []   
    environmentDataWanderRate = []          
    environmentDataSmellToSenseRatio = []   
    environmentDataStrength  = []
    environmentDataIntimidationFactor = []  
    environmentDataPerceptionAccuracy = []
    environmentDataProcreateModifier = []
    environmentDataAttackModifier = []
    environmentDataMinHunger = []  

    for ag in range(len(environmentDataAgents)-1):
        genome = Genome(f.readline())

        environmentDataSpeed.append(genome.stats["speed"])
        environmentDataVitality.append(genome.stats["vitality"])
        environmentDataStarvingDamage.append(genome.stats["starvingDamage"])
        environmentDataWanderRate.append(genome.stats["wanderRate"])
        environmentDataSmellToSenseRatio.append(genome.stats["smellToSenseRatio"])
        environmentDataStrength.append(genome.stats["strength"])
        environmentDataIntimidationFactor.append(genome.stats["intimidationFactor"])
        environmentDataPerceptionAccuracy.append(genome.stats["perceptionAccuracy"])
        environmentDataProcreateModifier.append(genome.stats["procreateModifier"])
        environmentDataAttackModifier.append(genome.stats["attackModifier"])
        environmentDataMinHunger.append(genome.stats["minHunger"])

        genomeData.append(genome)

    DataSpeed.append(environmentDataSpeed)
    DataVitality.append(environmentDataVitality)
    DataStarvingDamage.append(environmentDataStarvingDamage)
    DataWanderRate.append(environmentDataWanderRate)
    DataSmellToSenseRatio.append(environmentDataSmellToSenseRatio)
    DataStrength.append(environmentDataStrength)
    DataIntimidationFactor.append(environmentDataIntimidationFactor)
    DataPerceptionAccuracy.append(environmentDataPerceptionAccuracy)
    DataProcreateModifier.append(environmentDataProcreateModifier)
    DataAttackModifier.append(environmentDataAttackModifier)
    DataMinHunger.append(environmentDataMinHunger)

    AverageDataSpeed.append(genome.average_stats["speed"])
    AverageDataVitality.append(genome.average_stats["vitality"])
    AverageDataStarvingDamage.append(genome.average_stats["starvingDamage"])
    AverageDataWanderRate.append(genome.average_stats["wanderRate"])
    AverageDataSmellToSenseRatio.append(genome.average_stats["smellToSenseRatio"])
    AverageDataStrength.append(genome.average_stats["strength"])
    AverageDataIntimidationFactor.append(genome.average_stats["intimidationFactor"])
    AverageDataPerceptionAccuracy.append(genome.average_stats["perceptionAccuracy"])
    AverageDataProcreateModifier.append(genome.average_stats["procreateModifier"])
    AverageDataAttackModifier.append(genome.average_stats["attackModifier"])
    AverageDataMinHunger.append(genome.average_stats["minHunger"])


printGraph(environmentDataDays, AverageDataSpeed, 'days', 'average speed', 'Average speed per day', 4)
printGraph(environmentDataDays, AverageDataVitality, 'days', 'average vitality', 'Average vitality per day', 5)
printGraph(environmentDataDays, AverageDataStarvingDamage, 'days', 'average starvingDamage', 'Average starvingDamage per day', 6)
printGraph(environmentDataDays, AverageDataWanderRate, 'days', 'average wanderRate', 'Average wanderRate per day', 7)
printGraph(environmentDataDays, AverageDataSmellToSenseRatio, 'days', 'average smellToSenseRatio', 'Average smellToSenseRatio per day', 8)
printGraph(environmentDataDays, AverageDataStrength, 'days', 'average strength', 'Average strength per day', 9)
printGraph(environmentDataDays, AverageDataIntimidationFactor, 'days', 'average intimidationFactor', 'Average intimidationFactor per day', 10)
printGraph(environmentDataDays, AverageDataPerceptionAccuracy, 'days', 'average perceptionAccuracy', 'Average perceptionAccuracy per day', 11)
printGraph(environmentDataDays, AverageDataProcreateModifier, 'days', 'average procreateModifier', 'Average procreateModifier per day', 12)
printGraph(environmentDataDays, AverageDataAttackModifier, 'days', 'average attackModifier', 'Average attackModifier per day', 13)
printGraph(environmentDataDays, AverageDataMinHunger, 'days', 'average minHunger', 'Average minHunger per day', 14)

#procreate modifier and attack modifier
printScatterPlot(DataProcreateModifier[0], DataAttackModifier[0], 'Procreate Modifier', 'Attack Modifier', 'procreate modifier and attack modifier comparision (Day {s})'.format(s=1), 'proc_att_{s}'.format(s=i), 0, 0, 1, 1)

#for i in [10, 20, 30, 40, 50, 60, 70, 80, 90, 100]:
    #printScatterPlot(DataProcreateModifier[i-1], DataAttackModifier[i-1], 'Procreate Modifier', 'Attack Modifier', 'procreate modifier and attack modifier comparision (Day {s})'.format(s=i), 'proc_att_{s}'.format(s=i), 0, 0, 1, 1)

#Day1 Speed and Strength
printScatterPlot(DataSpeed[0], DataStrength[0], 'Speed', 'Strength', 'Speed and Strength comparision (Day {s})'.format(s=1), 'proc_att_{s}'.format(s=i), 0, 0, 1, 1)

for i in [10, 20, 30, 40, 50, 60, 70, 80, 90, 100]:
    printScatterPlot(DataSpeed[0], DataStrength[0], 'Speed', 'Strength', 'Speed and Strength comparision (Day {s})'.format(s=i), 'proc_att_{s}'.format(s=i), 5, 0, 20, 1)


f.close