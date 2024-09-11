export class TeamMemberTimesheetRow {
    id: number = 0;
    name: string = "";
    trackTime: boolean = true;
    hoursAdjustment: number = 0;
    createdOn: Date = new Date();
    updatedOn: Date = new Date();
    deletedOn?: Date;
    hours: number = 0;
    distinctDays: number = 0;

    constructor(obj: any) {
        if(obj == null) { return; }

        Object.keys(obj).forEach(key => {
            this[key] = obj[key];
        });
    }
}