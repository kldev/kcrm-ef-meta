import React, { useCallback } from 'react';

import { DatePicker, IDatePickerStrings } from '@fluentui/react/lib/DatePicker';
import { useLocale } from 'i18n/useLocale';
import { AllTranslationKeys } from 'i18n/en';
import { DayOfWeek } from '@fluentui/react/lib/Calendar';

interface Props {
  placeholder?: string;
  value?: number | null;
  onSelectDate?: (date: Date | null | undefined) => void;
  label?: AllTranslationKeys;
}

const AppDatePicker: React.FunctionComponent<Props> = (props) => {
  const t = useLocale();

  const dayPickerStrings = useCallback(() => {
    const settings: IDatePickerStrings = {
      months: [
        t('January'),
        t('February'),
        t('March'),
        t('April'),
        t('May'),
        t('June'),
        t('July'),
        t('August'),
        t('September'),
        t('October'),
        t('November'),
        t('December'),
      ],

      shortMonths: [
        t('Jan'),
        t('Feb'),
        t('Mar'),
        t('Apr'),
        t('MayShort'),
        t('Jun'),
        t('Jul'),
        t('Aug'),
        t('Sep'),
        t('Oct'),
        t('Nov'),
        t('Dec'),
      ],

      days: [
        t('Sunday'),
        t('Monday'),
        t('Tuesday'),
        t('Wednesday'),
        t('Thursday'),
        t('Friday'),
        t('Saturday'),
      ],

      shortDays: [
        t('SundayShort'),
        t('MondayShort'),
        t('TuesdayShort'),
        t('WednesdayShort'),
        t('ThursdayShort'),
        t('FridayShort'),
        t('SaturdayShort'),
      ],

      goToToday: t('GoToToday'),
      prevMonthAriaLabel: t('GoToPrevMonth'),
      nextMonthAriaLabel: t('GotToNextMonth'),
      prevYearAriaLabel: t('GoToPrevYear'),
      nextYearAriaLabel: t('GoToNextYear'),
      closeButtonAriaLabel: t('CloseDatePicker'),
    };

    return settings;
  }, [t]);

  const { value, placeholder, onSelectDate, label } = props;
  return (
    <DatePicker
      label={label ? t(label) : t('SelectDate')}
      firstDayOfWeek={DayOfWeek.Monday}
      strings={dayPickerStrings()}
      onSelectDate={onSelectDate}
      value={value && value > 0 ? new Date(value) : undefined}
      placeholder={placeholder}
      ariaLabel={placeholder}
    />
  );
};

export default AppDatePicker;
