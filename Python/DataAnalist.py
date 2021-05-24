import matplotlib.pyplot as pl


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
        self.statsList = statsLine.split(" ")
        self.stats ={
            "speed"                 : self.statsList[0],
            "vitality"              : self.statsList[1],
            "starvingDamage"        : self.statsList[2],
            "wanderRate"            : self.statsList[3],
            "smellToSenseRatio"     : self.statsList[4],
            "strength"              : self.statsList[5],
            "intimidationFactor"    : self.statsList[6],
            "perceptionAccuracy"    : self.statsList[7],
            "procreateModifier"     : self.statsList[8],
            "attackModifier"        : self.statsList[9],
            "minHunger"             : self.statsList[10]
        }
        for key in Genome.average_stats:
            Genome.average_stats[key] = update_avg(self.stats[key], Genome.average_stats[key], Genome.count)


def update_avg(sample, current_average, sample_count):
    return current_average + (sample - current_average)/sample_count

f = open("C:/Users/nocas/Documents/MultiAgentEcosystem/Data/gamesave.txt", "r") 

gamedata = f.readlines()

environmentData = []
environmentDataDays = []
environmentDataAgents = []
environmentDataFood = []

genomeData = []

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

printGraph(environmentDataDays, environmentDataAgents, 'days', 'agents', 'Number of agents per day', 1)

printGraph(environmentDataDays, environmentDataFood, 'days', 'food', 'Number of food per day', 2)

f.close
