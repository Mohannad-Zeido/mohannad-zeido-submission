import { Table, Typography, Layout } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import { investors, type Investor } from '../../data/Investors.ts';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'antd';

const { Title } = Typography;
const { Content } = Layout;

const columns: ColumnsType<Investor> = [
    {
        title: 'Investor Name',
        dataIndex: 'name',
        key: 'name',
        responsive: ['xs', 'sm', 'md', 'lg', 'xl'],
    },
    {
        title: 'Type',
        dataIndex: 'type',
        key: 'type',
        responsive: ['lg'], // show only on large screens and up
    },
    {
        title: 'Country',
        dataIndex: 'country',
        key: 'country',
        responsive: ['lg'],
    },
    {
        title: 'Commitment',
        key: 'commitment',
        responsive: ['md'],
        render: (_, record) =>
            `${record.commitmentAmount.toLocaleString()} ${record.commitmentCurrency}`,
    },
];

export default function Investors() {
    const navigate = useNavigate();
    const [isMobile, setIsMobile] = useState(false);

    useEffect(() => {
        const handleResize = () => {
            setIsMobile(window.innerWidth <= 991);
        };

        handleResize();
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);

    const expandedRowRender = (record: Investor) => (
        <div className="expanded-details">
            <p><strong>Type:</strong> {record.type}</p>
            <p><strong>Country:</strong> {record.country}</p>
            <p><strong>Commitment:</strong> {record.commitmentAmount} {record.commitmentCurrency}</p>
            <Button
                type="default"
                onClick={() => navigate('/investor-commitments')}
                style={{ marginTop: '8px' }}
            >
                View Commitments
            </Button>
        </div>
    );

    return (
        <Layout className={"layout-style"}>
            <Content>
                <Title level={2} style={{ textAlign: 'center' }}>Investors</Title>
                    <Table
                        columns={columns}
                        dataSource={investors}
                        rowKey="id"
                        expandable={
                            isMobile ? {
                                expandedRowRender,
                                expandRowByClick: true,
                            } : undefined}
                        pagination={{ pageSize: 10 }}
                        className="investor-table"
                        rowClassName={(_, index) =>
                            index % 2 === 0 ? 'even-row' : 'odd-row'
                        }
                        onRow={
                            isMobile ? undefined :() => ({
                            onClick: () => navigate('/investor-commitments'),
                            style: { cursor: 'pointer' },
                        })
                    }
                    />
            </Content>
        </Layout>
    );
}
