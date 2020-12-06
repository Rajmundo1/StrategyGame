export interface MyUnitSpecificsDto{
    id?: string | undefined;
    name?: string | undefined;
    imageUrl?: string | undefined;
    level: number;
    attackPower: number;
    defensePower: number;
    forceLimit: number;
    woodCost: number;
    marbleCost: number;
    wineCost: number;
    sulfurCost: number;
    goldCost: number;
    woodUpkeep: number;
    marbleUpkeep: number;
    wineUpkeep: number;
    sulfurUpkeep: number;
    goldUpkeep: number;
    desiredCount: number;
}