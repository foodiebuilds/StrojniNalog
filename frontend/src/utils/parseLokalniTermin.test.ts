import { describe, expect, it } from 'vitest'
import { parseLokalniTermin } from './parseLokalniTermin'

describe('parseLokalniTermin', () => {
  it('vrne datum za veljaven zapis', () => {
    const datum = parseLokalniTermin('2026-11-24T08:30')

    expect(datum).not.toBeNull()
    expect(datum?.getFullYear()).toBe(2026)
    expect(datum?.getMonth()).toBe(10)
    expect(datum?.getDate()).toBe(24)
    expect(datum?.getHours()).toBe(8)
    expect(datum?.getMinutes()).toBe(30)
  })

  it('zavrne neobstoječ datum', () => {
    expect(parseLokalniTermin('2026-02-30T10:00')).toBeNull()
  })

  it('zavrne napačen zapis', () => {
    expect(parseLokalniTermin('24. 11. 2026 08:30')).toBeNull()
  })
})
