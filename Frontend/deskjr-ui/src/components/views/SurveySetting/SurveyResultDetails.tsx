import { Accordion, AccordionTab } from 'primereact/accordion';
import { showErrorToast } from '../../../utils/toastHelper';
import { useEffect, useState } from 'react';
import employeeAnswersService from '../../../services/EmployeeAnswersService';
import { Chart } from 'primereact/chart';

const SurveyResultDetails = (props: any) => {
    const [surveyResult, setSurveyResult] = useState<any>();
    const [typeChart, setTypeChart] = useState("bar");
    const colorList = ['#5aa5a0', '#c1453e', '#9ae817', '#b739c6', '#2e84d1', '#fa05c1', '#fb9704'];

    useEffect(() => {
        getSurveyResults(props.selectedItemId);
    }, [props.selectedItemId]);

    const getSurveyResults = async (surveyId: any) => {
        if (surveyId) {
            try {
                const data = await employeeAnswersService.getSurveyResults(surveyId);
                setSurveyResult(data);
            } catch (err) {
                showErrorToast(err);
            }
        }
    };

    const options = {
        indexAxis: 'y',
        maintainAspectRatio: false,
        aspectRatio: 1.5,
        scales: {
            x: {
                ticks: {
                    font: {
                        weight: 500
                    }
                },
                grid: {
                    display: false,
                    drawBorder: false
                }
            },
            y: {
                ticks: {
                    font: {
                        weight: 800,
                    }
                },
                grid: {
                    drawBorder: false
                }
            }
        }
    };

    return (
        <>
            <div className="modal fade show d-block" id="questionFormModal" role="dialog" data-backdrop="static" style={{ backgroundColor: "rgba(0,0,0,0.5)", overflow: 'auto' }}>
                <div className="modal-dialog modal-lg" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">{surveyResult && surveyResult.name} Result</h5>
                            <button type="button" className="close" onClick={props.onClose}>
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div className="card flex p-2">
                            <select className="custom-select custom-select-md mb-2" onChange={(e) => setTypeChart(e.target.value)}>
                                <option value="bar">Bar Chart</option>
                                <option value="pie">Pie Chart</option>
                                <option value="doughnut">Doughnut Chart</option>
                            </select>
                        </div>
                        <div className="modal-body">
                            <div>
                                {surveyResult && (
                                    <Accordion multiple>
                                        {surveyResult.questions.map((question, index) => {
                                            const questionData = {
                                                labels: question.options.map((option: any) => option.text),
                                                datasets: [
                                                    {
                                                        label: 'Count',
                                                        backgroundColor: colorList,
                                                        borderColor: colorList,
                                                        data: question.options.map((option: any) => option.answerCount)
                                                    }
                                                ]
                                            };
                                            return (
                                                <AccordionTab header={question.text} key={index}>
                                                    <ul>
                                                        <Chart type={typeChart} data={questionData} options={options} />
                                                    </ul>
                                                </AccordionTab>
                                            );
                                        })}
                                    </Accordion>
                                )}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default SurveyResultDetails;
