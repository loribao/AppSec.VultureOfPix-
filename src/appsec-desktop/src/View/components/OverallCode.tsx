import { Card, Col, Container, Row, Table } from "react-bootstrap";
import { gql, useQuery } from '@apollo/client';
import styled from "styled-components";
import Plot from 'react-plotly.js';
export interface dataOverallCode {
    data: Data
}

export interface Data {
    overallCode: OverallCode
}

export interface OverallCode {
    component: Component
    metrics: Metric[]
    period: Period2
}

export interface Component {
    key: string
    name: string
    qualifier: string
    measures: Measure[]
}

export interface Measure {
    bestValue: boolean
    metric: string
    value: string
    period?: Period
}

export interface Period {
    bestValue: boolean
    date: any
    index: number
    mode: string
    value: string
}

export interface Metric {
    bestValue: string
    decimalScale?: number
    description: string
    domain: string
    hidden: boolean
    higherValuesAreBetter: boolean
    key: string
    name: string
    qualitative: boolean
    type: string
    worstValue: string
}

export interface Period2 {
    bestValue: boolean
    date: string
    index: number
    mode: string
    value: string
}

const GETOverallCode = gql`
  query getOverallCode($projectName: String!){
    overallCode(projectName: $projectName) {
      component {
        key
        name
        qualifier
        measures {
          bestValue
          metric
          value
          period {
            bestValue
            date
            index
            mode
            value
          }
        }
      }
      metrics {
        bestValue
        decimalScale
        description
        domain
        hidden
        higherValuesAreBetter
        key
        name
        qualitative
        type
        worstValue
      }
      period {
        bestValue
        date
        index
        mode
        value
      }
    }
  }
`;

const CardStyleVulture = styled.div`
    --bs-card-spacer-y: 1rem;
    --bs-card-spacer-x: 1rem;
    --bs-card-title-spacer-y: 0.5rem;
    --bs-card-title-color: ;
    --bs-card-subtitle-color: ;
    --bs-card-border-width: var(--bs-border-width);
    --bs-card-border-color: var(--bs-border-color-translucent);
    --bs-card-border-radius: var(--bs-border-radius);
    --bs-card-box-shadow: ;
    --bs-card-inner-border-radius: calc(var(--bs-border-radius) - (var(--bs-border-width)));
    --bs-card-cap-padding-y: 0.5rem;
    --bs-card-cap-padding-x: 1rem;
    --bs-card-cap-bg: rgba(var(--bs-body-color-rgb), 0.03);
    --bs-card-cap-color: ;
    --bs-card-height: ;
    --bs-card-color: ;
    --bs-card-bg: var(--bs-body-bg);
    --bs-card-img-overlay-padding: 1rem;
    --bs-card-group-margin: 0.75rem;
    position: relative;
    display: flex;
    box-shadow: 0 .5rem 1rem #544692;
    flex-direction: column;
    min-width: 0;
    color: var(--color-title-one);
    word-wrap: break-word;
    background-color: #322a57;
    background-clip: border-box;
    border: var(--bs-card-border-width) solid var(--bs-card-border-color);
    border-radius: var(--bs-card-border-radius);
    margin: 0;
    padding: 0;
    align-content: center;
    width: 100%;
`

const RowStyleVulture = styled.div`
    display: flex;
    flex-wrap: wrap;
    margin: 0;
    padding: 0;
    width: 100%;
    gap: 1em;
`
const CenteredRowStyleVulture = styled(RowStyleVulture)`
  display: flex;
  justify-content: space-between;
  flex-wrap: wrap;
`;
function OverView(_dataOverallCode: dataOverallCode) {
    const _data = [
        { values: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10], color: "blue" },
        { values: [0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20], color: "red" }
    ];
    const plotData = _data.map(line => ({
        x: line.values.map((_, i) => i),
        y: line.values,
        mode: 'lines',
        name: line.color,
        line: { color: line.color }
    }));
    return (<RowStyleVulture>
                <table className="table-responsive table-bordered table-dark table-hover" style={{ overflow: "auto", width: "100%" }}>
                    <thead>
                        <tr>
                            <th>Metric</th>
                            <th>Value</th>
                        </tr>
                    </thead>
                    <tbody>
                        {_dataOverallCode.data.overallCode.component.measures.filter(x => (x.value?.length !== 0 || !!x.value) && x.value.trim() !== "quality_gate_details").map((metric, i) => {
                            return (
                                <tr key={i}>
                                    <td>{metric.metric}</td>
                                    <td>{metric.value}</td>
                                </tr>
                            )
                        })}
                    </tbody >
                </table>


    </RowStyleVulture>)
}

export function OverallCode() {
    const { loading, error, data } = useQuery(GETOverallCode, {
        variables: { projectName: "appsec-server" },
    });

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error :(</p>;
    console.log(data)
    return <OverView data={data} />
}
