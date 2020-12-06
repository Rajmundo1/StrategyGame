export interface MyUnitDto{
    unitSpecificsId: string;
    name?: string | undefined;
    desiredCount?: number;
    count: number;
    level: number;
    maxLevel: number;
    imageUrl?: string | undefined;
}