export interface ICountryModel{
    name: string;
    coral: number;
    pearl: number;
    upgradeTimeLeft: number;
    buildingTimeLeft: number;
    score: number;
    buildinGroupID: number;
    armyID: number;
    upgradeID: number;
}

export interface IUnitViewModel{
    name: string;
    count: number;
    imageUrl: string;
}
