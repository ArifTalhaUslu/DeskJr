import React from "react";
import { User } from "../../../types/user";


const OrganizationalChart : any = (props:any) => {

    return (
        <div className="container mt-4">
            <div className="row">
                <div className="col-12 mx-auto">
                    <div className="card bg-primary text-white mb-4">
                        <div className="card-body">
                            <h1 className="card-title">
                                Organizational Chart
                            </h1>
                        </div>
                    </div>
                </div>
                <div className="col-12 mx-auto">
                    <div className="card">
                        <div className="card-header">
                            <h2>aaa</h2>
                        </div>
                        <div className="card-body">
                            <table className="table table-striped">
                                <tbody>
                                    <tr>
                                        <th>Name:</th>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>       
            </div>
        </div>
    );
};
export default OrganizationalChart;