import { describe, expect, it } from 'vitest'
import { validirajNarocilo } from './narociloValidation'

describe('validirajNarocilo', () => {
  it('označi vse manjkajoče podatke', () => {
    const napake = validirajNarocilo({
      vrstaStoritveId: '',
      opisDela: ' ',
      lokacija: '',
      zeleniTermin: '',
    })

    expect(Object.keys(napake).sort()).toEqual([
      'lokacija',
      'opisDela',
      'vrstaStoritveId',
      'zeleniTermin',
    ])
  })

  it('sprejme pravilno izpolnjeno naročilo', () => {
    const napake = validirajNarocilo({
      vrstaStoritveId: '1',
      opisDela: 'Izkop jarka',
      lokacija: 'Maribor',
      zeleniTermin: '2026-12-01T08:00',
    })

    expect(napake).toEqual({})
  })
})
