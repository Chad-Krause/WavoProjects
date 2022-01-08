export class TeamMemberTimesheetRow {
    id: number;
    name: string;
    trackTime: boolean;
    hoursAdjustment: number;
    createdOn: Date;
    updatedOn: Date;
    deletedOn: Date;
    hours: number;
    distinctDays: number;

    constructor(obj: any) {
        if(obj == null) { return; }

        Object.keys(obj).forEach(key => {
            this[key] = obj[key];
        });
    }
}