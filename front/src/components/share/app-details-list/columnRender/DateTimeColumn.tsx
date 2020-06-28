import React from 'react';
import moment from 'moment';

interface Props {
  value: Date | null;
  format?: string;
}

const DateTimeColumn: React.FC<Props> = (props) => {
  return props.value ? (
    <>{moment(props.value).format(props.format || 'YYYY-MM-DD')}</>
  ) : null;
};

export default DateTimeColumn;
