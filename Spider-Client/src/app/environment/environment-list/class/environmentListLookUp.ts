import { Application } from '../../../application/classes/application';
import { Environment } from '../../classes/environment';

export class EnvironmentListLookUp {
    totalEnvironmets: number;
    application: Application;
    environments: Environment[];
    constructor() {
        this.application = new Application();
    }
}
