import { Table, Typography, Layout } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import {investorCommitments, type InvestorCommitments} from '../../data/InvestorCommitments.ts';
import {useState} from "react";
import { Select } from 'antd';
import { useNavigate } from 'react-router-dom';
import { Button } from 'antd';

const { Title } = Typography;
const { Content } = Layout;

const assetClassOptions = Array.from(
    new Set(investorCommitments.map(c => c.assetClass))
);


const columns: ColumnsType<InvestorCommitments> = [
    {
        title: 'Investor',
        dataIndex: 'investorName',
        key: 'investorName',
    },
    {
        title: 'Country',
        dataIndex: 'investorCountry',
        key: 'investorCountry',
        responsive: ['md'],
    },
    {
        title: 'Asset Class',
        dataIndex: 'assetClass',
        key: 'assetClass',
    },
    {
        title: 'Amount',
        key: 'amount',
        render: (_, record) =>
            `${record.amount.toLocaleString()} ${record.currency}`,
    },
];

export default function CommitmentsPage() {

    const navigate = useNavigate();
    const [selectedAssetClasses, setSelectedAssetClasses] = useState<string[]>([]);

    const filteredCommitments = selectedAssetClasses.length > 0
        ? investorCommitments.filter(c => selectedAssetClasses.includes(c.assetClass))
        : investorCommitments;



    return (
        <Layout className={"layout-style"}>
            <Content>
                <Title level={2} style={{ textAlign: 'center' }}>Investor Commitments</Title>
                <Select
                    className="asset-class-filter"
                    mode="multiple"
                    placeholder="Filter by Asset Class"
                    allowClear
                    value={selectedAssetClasses}
                    onChange={(values) => setSelectedAssetClasses(values)}
                >
                    {assetClassOptions.map(assetClass => (
                        <Select.Option key={assetClass} value={assetClass}>
                            {assetClass}
                        </Select.Option>
                    ))}
                </Select>
                <Table
                    columns={columns}
                    dataSource={filteredCommitments}
                    rowKey="id"
                    pagination={{ pageSize: 10 }}
                    className="commitments-table"
                    rowClassName={(_, index) =>
                        index % 2 === 0 ? 'even-row' : 'odd-row'
                    }
                />
                <Button
                    type="default"
                    onClick={() => navigate('/')}
                    style={{ marginBottom: 16 }}
                >
                    Back to Investors
                </Button>
            </Content>
        </Layout>
    );
}
