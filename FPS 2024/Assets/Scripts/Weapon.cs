using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponModel weapon;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletImpact;
    [SerializeField] int magazine;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshRenderer meshRenderer;
    float timeBetweenShoots;
    bool reload;

    private void Start()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        magazine = weapon.MagazineCap;

        meshFilter.mesh = weapon.Model;
        meshRenderer.material = weapon.Material;
    }

    void fire()
    {
        FireCouroutine();
    }

    private IEnumerator FireCouroutine()
    {
        if (magazine > 0 && weapon.FireRate > 0 )
        {
            magazine--;
        }
        yield return null;
        Shoot();
    }


    private void Shoot()
    {
        RaycastHit hit;

        Vector3 direction = new Vector3(Random.Range(-weapon.Spread, weapon.Spread), Random.Range(-weapon.Spread, weapon.Spread), 0);
        float range = weapon.Range;

        if (Physics.Raycast(firePoint.position, direction, out hit, range))
        {
            Collider obj = hit.transform.GetComponent<Collider>();
            if (obj != null)
            {
                Debug.Log(obj.gameObject.name);
                Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }

        Debug.DrawLine(firePoint.position, firePoint.position + direction * range);
    }

    void Reload()
    {
        ReloadCoroutine();
    }

    private IEnumerator ReloadCoroutine()
    {
        // Verifica se precisa recarregar e se há munição disponível
        if(magazine != weapon.MagazineCap)
        {
            reload = true;
        }

        // Atualiza a munição no carregador e no inventário

        // Aguarda o tempo de recarga
        yield return null;
    }

}
