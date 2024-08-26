import React, { useState } from "react";
import { User } from "../../../types/user";
import { OrganizationChart } from 'primereact/organizationchart';
import { TreeNode } from 'primereact/treenode';
import 'primereact/resources/themes/saga-blue/theme.css';
import 'primereact/resources/primereact.min.css';
import 'primeicons/primeicons.css';  

function OrganizationCharts(props: any) {

    const [selection, setSelection] = useState([]);
    const data = [{
        label: 'CEO',
        type: 'person',
        className: 'p-person',
        expanded: true,
        data: { name: 'Walter White' },
        children: [
            {
                label: 'CFO',
                type: 'person',
                className: 'p-person',
                expanded: true,
                data: { name: 'Saul Goodman' },
                children: [{
                    label: 'Tax',
                    className: 'department-cfo'
                },
                {
                    label: 'Legal',
                    className: 'department-cfo'
                }],
            },
            {
                label: 'COO',
                type: 'person',
                className: 'p-person',
                expanded: true,
                data: { name: 'Mike E.' },
                children: [{
                    label: 'Operations',
                    className: 'department-coo'
                }]
            },
            {
                label: 'CTO',
                type: 'person',
                className: 'p-person',
                expanded: true,
                data: { name: 'Jesse Pinkman' },
                children: [{
                    label: 'Development',
                    className: 'department-cto',
                    expanded: true,
                    children: [{
                        label: 'Analysis',
                        className: 'department-cto'
                    },
                    {
                        label: 'Front End',
                        className: 'department-cto'
                    },
                    {
                        label: 'Back End',
                        className: 'department-cto'
                    }]
                },
                {
                    label: 'QA',
                    className: 'department-cto'
                },
                {
                    label: 'R&D',
                    className: 'department-cto'
                }]
            }
        ]
    }];

    const nodeTemplate = (node) => {
        if (node.type === "person") {
            return (
                <div>
                    <div className="node-header">{node.label}</div>
                    <div className="node-content">
                        <div>{node.data.name}</div>
                    </div>
                </div>
            );
        }

        return node.label;
    }
    return (
        <div className="organizationchart-demo">
            <div className="card">
                <OrganizationChart value={data} nodeTemplate={nodeTemplate} selection={selection} selectionMode="multiple"
                    onSelectionChange={event => setSelection(newFunction(event))} className="company"></OrganizationChart>
            </div>
        </div>
    );

    function newFunction(event): React.SetStateAction<any[]> {
        return event.data;
    }
}
export default OrganizationCharts;